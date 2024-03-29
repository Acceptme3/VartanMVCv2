﻿using VartanMVCv2.Domain;
using VartanMVCv2.Domain.Entities;
using VartanMVCv2.Views;

namespace VartanMVCv2.ViewModels
{
    public class IndexViewModel
    {
        public _ViewStartModel _viewStartModel = new _ViewStartModel();

        public Client? ClientExample { get; set; } = null;
        public Feedback? FeedbackExample { get; set; } = null;
        public WorkServices? WorkServicesExample { get; set; } = null;
        public CompletedProject? CompletedProjectExample { get; set; } = null;

        public IEnumerable<WorksCategory> sortWorksList { get; set; } = new List<WorksCategory>();
        public IEnumerable<Feedback> sortFeedbackList { get; set; } = new List<Feedback>(); 

        private readonly Modelinitializer? _initializer;

        public  DataModel? dataModelExample;

        public IndexViewModel() 
        {
           
        }

        public  IndexViewModel(  Modelinitializer modelinitializer) 
        {        
            _initializer = modelinitializer;
            dataModelExample = _initializer.GetModelObject();
        }

        public void SetInstance(DataModel model)
        {
            dataModelExample = model;                   
        }
       
    }
}
