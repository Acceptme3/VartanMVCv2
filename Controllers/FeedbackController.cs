using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using VartanMVCv2.Domain;
using VartanMVCv2.ViewModels;

namespace VartanMVCv2.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly ILogger<FeedbackController> _logger;
        private readonly AplicationDBContext _dbContext;
        private readonly DataManager _dataManager;

        public FeedbackController(ILogger<FeedbackController> logger, AplicationDBContext context, DataManager dataManager) 
        {
            _dataManager = dataManager;
            _logger = logger;   
            _dbContext = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedbackAsync(IndexViewModel feedback)
        {
            // _indexViewModel.FeedbackExample = feedback.FeedbackExample;
            _logger.LogInformation("Начинает выполнение Home/AddFeedbackAsync, [тип запроса: POST]");

            _logger.LogInformation($"Все поля модели: Имя {feedback.FeedbackExample!.FeedbackClientName} + \n" +
                $"Телефон {feedback.FeedbackExample.FeedbackPhone}+\n" +
                $"Текст отзыва {feedback.FeedbackExample.FeedbackText}");
            foreach (var item in ModelState)
            {
                _logger.LogInformation($"Пара ключ значение {item}");
            }


            if (ModelState.IsValid)
            {
                _logger.LogInformation("Екземпляр отзыва клиента успешно прошел валидацию на стороне сервера [VALIDATE]");
                await _dataManager.FeedbackRepository.AddedAsync(feedback.FeedbackExample);
                return RedirectToAction("ConfirmFeedback", "Home");
            }
            _logger.LogInformation("Екземпляр клиента НЕ прошел валидацию на стороне сервера [NOT VALID]");
            string validErrors = "";
            foreach (var item in ModelState)
            {
                // если для определенного элемента имеются ошибки
                if (item.Value.ValidationState == ModelValidationState.Invalid)
                {
                    validErrors = $"{validErrors}\n Ошибки для свойства {item.Key}:\n";
                    _logger.LogInformation(validErrors);
                    // пробегаемся по всем ошибкам
                    foreach (var error in item.Value.Errors)
                    {
                        validErrors = $"{validErrors}{error.ErrorMessage}\n";
                        _logger.LogInformation(validErrors);
                    }
                }
            }
            //_indexViewModel.FeedbackExample = feedback.FeedbackExample;
            return RedirectToAction("Index", "Home", feedback);
        }
    }
}
