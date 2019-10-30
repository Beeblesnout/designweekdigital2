using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class IDedObject<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// Private storage for the ID.
    /// </summary>
    [SerializeField]
    [Tooltip("Unique ID for this object. <RMB for tools>")]
    [ContextMenuItem("Validate This ID", "ValidateID")]
    [ContextMenuItem("Assign Next Available ID", "ReassignID")]
    private byte p_ID;

    /// <summary>
    /// Readonly byte used to identify this object.
    /// </summary>
    public byte ID { get => p_ID; }

    /// <summary>
    /// Reassigns the object's ID to the first available ID.
    /// </summary>
    /// <returns>The previous ID.</returns>
    protected byte ReassignID()
    {
        byte prevID = p_ID;
        if (allIDs.Count == 0) 
        {
            p_ID = 0;
        }
        else
        {
            for (byte i = 0; i < byte.MaxValue; i++)
            {
                if (!allIDs.Contains(i))
                {
                    p_ID = i;
                    break;
                }
            }
            Console.Warn("[IDReassignment] There are no more available IDs to be assigned, trying to reach IDed objects might return the wrong objects you were intending.");
        }
        return prevID;
    }

    /// <summary>
    /// Checks if the ID has already been used. Reassign a new one if it has.
    /// </summary>
    /// <returns>The new ID if needed to be reassigned.</returns>
    public byte ValidateID()
    {
        if (allIDs.Contains(p_ID))
        {
            byte prevID = ReassignID();
            Console.Warn("[IDReassignment] An object already exists with the ID of " + prevID + ". " + gameObject.name + "'s ID has been automatically reassigned to " + p_ID);
        }
        return p_ID;
    }

    public virtual void Awake() {
        IDedObject<T>.AddObject(this);
        ValidateID();
        Console.WriteLine("IDedObject of type " + typeof(T) + " created with the ID: " + p_ID);
    }

    public virtual void OnDestroy()
    {
        IDedObject<T>.RemoveObject(this);
    }

    #region GLOBAL REFERENCING

    private static List<IDedObject<T>> allObjects = new List<IDedObject<T>>();
    private static List<byte> allIDs = new List<byte>();

    /// <summary>
    /// [Static] Adds the new object to a list and checks the legality of it's ID.
    /// </summary>
    /// <param name="newObject">The object to add.</param>
    public static void AddObject(IDedObject<T> newObject)
    {
        IDedObject<T>.allObjects.Add(newObject);
        allIDs.Add(newObject.ID);
    }

    /// <summary>
    /// [Static] Remove an object from the object and ID lists.
    /// </summary>
    /// <param name="deletingObject">The object to remove.</param>
    public static void RemoveObject(IDedObject<T> deletingObject)
    {
        allObjects.Remove(deletingObject);
        allIDs.Remove(deletingObject.ID);
    }

    /// <summary>
    /// [Static] Refreshes all allObjects list.
    /// </summary>
    public static void RefreshAllObjects() {
        allObjects = ((IDedObject<T>[])FindObjectsOfType(typeof(IDedObject<T>))).ToList();
        allIDs = allObjects.Select(obj => obj.ID).ToList();
    }

    /// <summary>
    /// [Static] Searches for an object with an equal ID to givenID.
    /// </summary>
    /// <param name="givenID">ID to search for.</param>
    /// <returns>Returns the found object with ID equal to givenID. Returns a default value of T if no object of givenID exists.</returns>
    public static IDedObject<T> GetObject(byte givenID)
    {
        if (savedObject) 
            if (savedObject.ID == givenID) 
                return savedObject;
        savedObject = allObjects.Find(obj => obj.ID == givenID);
        if (savedObject == default(IDedObject<T>)) Console.Warn("[ObjectIDSearch|Type:" + typeof(T) + "] No object of the type '" + typeof(T) + "' exists with the ID of " + givenID.ToString() + ". (Returning default value for " + typeof(T) + ")");
        return savedObject;
    }
    private static IDedObject<T> savedObject;

    #endregion
}