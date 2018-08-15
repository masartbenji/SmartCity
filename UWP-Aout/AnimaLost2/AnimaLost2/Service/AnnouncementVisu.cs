using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimaLost2.Service
{
    public class AnnouncementVisu
    {
        public int idAnnoun { get; set; }
        public DateTime DateAnnoun { get; set; }
        public string NameAnimal { get; set; }
        public string Breed { get; set; }
        public string Species { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public static ObservableCollection<AnnouncementVisu> Deserialize(string json)
        {
            ObservableCollection<AnnouncementVisu> announcements = new ObservableCollection<AnnouncementVisu>();
            if(json != "[]")
            {
                var splitAnnouncement = json.Split(new string[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var announ in splitAnnouncement)
                {
<<<<<<< HEAD
                    splitAnnouncement = announ.Split(new char[] { ',', '"', '{', '}', '[', ']', ':', '-', 'T' }, StringSplitOptions.RemoveEmptyEntries);
=======
                    var splitAnnouncement = announ.Split(new char[] { ',', '"', '{', '}', '[', ']', ':', '-', 'T' }, StringSplitOptions.RemoveEmptyEntries);
>>>>>>> parent of e405cc0... 15/08/18
                    AnnouncementVisu announcement = new AnnouncementVisu()
                    {
                        idAnnoun = Int32.Parse(splitAnnouncement[1]),
                        Breed = splitAnnouncement[12],
                        Species = splitAnnouncement[14],
                        Description = splitAnnouncement[16],
                        NameAnimal = splitAnnouncement[10],
                        Status = splitAnnouncement[18],
                        DateAnnoun = new DateTime(Int32.Parse(splitAnnouncement[3]), Int32.Parse(splitAnnouncement[4]), Int32.Parse(splitAnnouncement[5]))
                    };
                    announcements.Add(announcement);
                }
            }
            return announcements;
        }
    }
}
