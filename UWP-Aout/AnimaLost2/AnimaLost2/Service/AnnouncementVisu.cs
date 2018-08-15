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
                var split = json.Split(new string[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var announ in split)
                {
                        var splitAnnouncement = announ.Split(new char[] {'"', '{', '}', '[', ']', ':', '-' }, StringSplitOptions.RemoveEmptyEntries);
                        AnnouncementVisu announcement = new AnnouncementVisu()
                        {
                            idAnnoun = Int32.Parse(splitAnnouncement[1].Substring(0,1)),
                            Breed = splitAnnouncement[13],
                            Species = splitAnnouncement[16],
                            Description = splitAnnouncement[19],
                            NameAnimal = splitAnnouncement[10],
                            Status = splitAnnouncement[22],
                            DateAnnoun = new DateTime(Int32.Parse(splitAnnouncement[3]), Int32.Parse(splitAnnouncement[4]), Int32.Parse(splitAnnouncement[5].Substring(0, 2)))
                        };
                        announcements.Add(announcement);
                }
            }
            return announcements;
        }
    }
}
