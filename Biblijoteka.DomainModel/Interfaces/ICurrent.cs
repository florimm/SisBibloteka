// -----------------------------------------------------------------------
// <copyright file="ICurrent.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Biblioteka.DomainModel.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface ICurrent
    {
        User CurrentUser { get; set; }
    }
}
