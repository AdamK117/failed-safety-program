using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockApp
{
    public interface IEditableHolderT<TContent> : IHolderT<TContent>
    {
        new TContent Content { get; set; }
    }
}
