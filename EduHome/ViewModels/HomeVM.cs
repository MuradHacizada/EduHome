using EduHome.Models;
using System.Collections.Generic;

namespace EduHome.ViewModels
{
    public class HomeVM
    {
       public  List<Service>  Services { get; set; }   
        public List<Slider> Sliders { get; set; }
        public About About { get; set; }
        public List<CoursesWe> CoursesWes { get; set; }
        public List<NoticeBoard> NoticeBoards { get; set; }
        public List<UpEvent> UpEvents { get; set; }
        public List<Info> Infos { get; set; }   

    }
}
