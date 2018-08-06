using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimaLost2.Model
{
    public class Announcement
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        public int IdStatus { get; set; }
        public int IdAnimal { get; set; }

        public Animal IdAnimalNavigation { get; set; }
        public Status IdStatusNavigation { get; set; }
        public static ObservableCollection<Announcement> Deserialize(string json)
        {
            ObservableCollection<Announcement> announcements = new ObservableCollection<Announcement>();
            var splitAnnouncement = json.Split(new string[] { "},{" }, StringSplitOptions.RemoveEmptyEntries);
            foreach(var announ in splitAnnouncement)
            {
                splitAnnouncement = announ.Split(new char[] { ',', '"', '{', '}', '[', ']',':' }, StringSplitOptions.RemoveEmptyEntries);
                Announcement announcement = new Announcement()
                {
                    CoordX = Int32.Parse(splitAnnouncement[9]),
                    CoordY = Int32.Parse(splitAnnouncement[11]),
                    Description = splitAnnouncement[7],
                    IdStatus = Int32.Parse(splitAnnouncement[13]),
                    IdAnimal = Int32.Parse(splitAnnouncement[15]),
                    Id = Int32.Parse(splitAnnouncement[1])
                };
                announcements.Add(announcement);
            }
            return announcements;
        }
    }
}
