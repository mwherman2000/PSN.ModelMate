using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parallelspace.Content.Objects
{
    // Parallelspace Corporation Technical Note TN0028: SPS2003 XML Import-Export "operation" Attribute
    //
    //Operation Attribute Values Description Pre-delete Existing Item Create New Item
    //
    //operation=”delete”		Delete deletes an existing item imported into SPS2003 during a previous execution of the Import tool; or fails if the item doesn’t exist.No  No
    //operation =”remove”		Delete deletes an existing item imported into SPS2003 during a previous execution of the Import tool, if it exists.	No No
    //operation=”create”		Create creates a new instance of the exported item; or fails if the item already exists(see replace, update and add)   No Yes
    //operation=”replace”		Replace replaces an existing item imported intoSPS2003 during a previous execution of the Import tool by executing a Delete followed by a Create; or fails if the item doesn’t exit.Yes Yes
    //operation =”add”			Add a) creates a new instance of the exported item if the item does *not* exist; or b) replaces an item if the item was imported into SPS2003 during a previous execution of the Import tool. This operation shouldn’t fail based on previous import operations
    // a) No  a) Yes
    // b) Yes b) Yes
    //operation =”merge”		Merge a) creates a new instance of the exported item if the item does *not* exist; or b) updates an item if the item was imported into SPS2003 during a previous execution of the Import tool. This operation shouldn’t fail based on previous import operations
    // a) No a) Yes
    // b) No b) No
    // Merge local properties (update object fields), related properties (update related property sets), substructure (recursively update source child objects) 
    //
    //operation =”update”		Update adds or replaces the values of selected attributes of an existing item imported intoSPS2003 during a previous execution of the Import tool; or fails if the item doesn’t exit.No  No
    //operations =”ignore”		Processing for this item is skipped.The contents of the item are ignored.n/a n/a

    public enum PCOOperation
    {
        ignore,
        create,
        delete,
        remove,
        replace,
        update,
        add,
        merge,
        atrest,
        OtherOrUnknownOrUndefined
    }
}
