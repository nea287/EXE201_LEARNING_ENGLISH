using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_Client.ViewModel;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EXE201_LEARNING_ENGLISH_Client.Pages.Students.Course.Slot
{
    public class IndexModel : PageModel
    {
        private readonly ISlotService _slotService;

        public IndexModel(ISlotService slotService)
        {
            _slotService = slotService;
        }

        public int StudentCourseId;
        public IList<SlotViewModel> SlotViewModels { get; set; }

        public void OnGet()
        {
            string studentCourseId = Request.Query["CourseId"];
            if (int.TryParse(studentCourseId, out int courseId))
            {
                StudentCourseId = courseId;
            }
            var slots = _slotService.GetSlotsByStudentCourseId(StudentCourseId);
            SlotViewModels = new List<SlotViewModel>();
            foreach (var slot in slots)
            {
                SlotViewModel slotViewModel = new SlotViewModel
                {
                    Duration = slot.Duration,
                    EndTime = slot.EndTime,
                    IsAttend = slot.IsAttended,
                    StartTime = slot.StartTime
                };
                SlotViewModels.Add(slotViewModel);
            }
        }
    }
}
