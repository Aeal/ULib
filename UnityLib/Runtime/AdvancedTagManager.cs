using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
//this class holds information that the type will implement that the
//class may need from the type for tag options such as UniqueID


//purpose to allow users to register objects with string tags
//and to allow easy searching for objects with multiple tags
public class AdvancedTagManager : Singleton<AdvancedTagManager> 
{
    //public functions
    //get objects with -tag -tags -Type
    //add object  with -tag tags
    //remove object from tag -object -tag
    //remove object from tags -object -tags

    //sort the dictionary from shortest to longest 
    //find the shortest array out of all the tags
    //get the rest of the lists and chec to see if they contain the objects in the shortest array.
    
    private Dictionary<string, List<GameObject>> taggedObjects = new Dictionary<string,List<GameObject>>();
   
}

