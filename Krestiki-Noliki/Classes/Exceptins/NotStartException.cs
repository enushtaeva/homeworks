using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Krestiki_Noliki.Classes.Exceptins
{
    [System.Serializable]
    public class NotStartException:ApplicationException
    {
        private bool _start;
        public bool Start
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
            }
        }

        public NotStartException(bool Start) : base("Вы не нажали кнопку старт") {
            this.Start = Start;
        }

        public NotStartException(string message, bool Start) : base(message) {
            this.Start = Start;
        }

        public NotStartException(string message, Exception inner,bool Start) : base(message, inner) {
            this.Start = Start;
        }

        protected NotStartException(SerializationInfo info, StreamingContext context) : base(info, context) {
            if (info != null)
            {
                this._start = info.GetBoolean("Start");
            }
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("Start", this.Start);
        }
    }
}
