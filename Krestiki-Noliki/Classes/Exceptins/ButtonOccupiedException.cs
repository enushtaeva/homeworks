using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Krestiki_Noliki.Classes.Exceptins
{
    class ButtonOccupiedException:ApplicationException
    {
        private  Image buttonImage;
        public Image ButtonImage
        {
            get
            {
                return buttonImage;
            }
            set
            {
                buttonImage = value;
            }
        }

        public ButtonOccupiedException(Image image) : base("Кнопка уже занята") {
            this.ButtonImage = image;
        }

        public ButtonOccupiedException(Image image,string message) : base(message) {
            this.ButtonImage = image;
        }

        public ButtonOccupiedException(Image image,string message, Exception inner) : base(message, inner) {
            this.ButtonImage = image;
        }

        protected ButtonOccupiedException(SerializationInfo info, StreamingContext context) : base(info, context) {
            if (info != null)
            {
                this.ButtonImage= (Image)(info.GetValue("ButtonImage",typeof(Image)));
            }
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("ButtonImage", this.ButtonImage);
        }
    }
}
