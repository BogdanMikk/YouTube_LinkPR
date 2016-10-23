using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_link_saver
{
    class Links
    {
        private int id;
        private string link;
        private string descr;
        private int rating;
        private string preview;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        public string Link
        {
            get
            {
                return link;
            }

            set
            {
                link = value;
            }
        }
        public string Descr
        {
            get
            {
                return descr;
            }

            set
            {
                descr = value;
            }
        }
        public int Rating
        {
            get
            {
                return rating;
            }

            set
            {
                rating = value;
            }
        }
        public string Preview
        {
            get
            {
                return preview;
            }

            set
            {
                preview = value;
            }
        }

        public Links(int id, string link, string descr, int rating, string preview)
        {
            this.id = id;
            this.link = link;
            this.descr = descr;
            this.rating = rating;
            this.preview = preview;
        }
    }
}
