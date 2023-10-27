using VartanMVCv2.Domain;
using VartanMVCv2.Domain.Entities;

namespace VartanMVCv2.Models
{
    public static class DataModelExtension
    {
        /*public static (List<WorksCategory>, List<Work>) GetAllAssocietedWorks(this DataModel dataModel, WorkServices services) 
        {
            List<WorksCategory> worksLists = dataModel.worksList.Where(entities => entities.Services == services).ToList();
            List<Work> worksNames = new List<Work>();
            foreach (WorksList category in worksLists)
            {
                worksNames.AddRange((List<Work>)dataModel.worksNameList.Where(entities => entities.WorksCategory == category));
            }
            return (worksLists, worksNames);
        }*/
    }
}
