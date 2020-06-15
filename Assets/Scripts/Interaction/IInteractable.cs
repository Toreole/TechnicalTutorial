using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial
{
    //an interface to mark an object as interactable
    public interface IInteractable
    {
        ///<summary>
        ///the method stub/template to make an object interact with the player which is passed as parameter.
        ///</summary>
        void Interact(PlayerController player);
    }
}
