using UnityEngine;


namespace BlueHood.Items.Data
{
    public enum ItemType { None, Weapon, Ammo, Flask, Coin }

    public interface IPickable
    {
        IUsable GetUsable();
        bool SetPickable(bool flag);
    }

    public interface IUsable
    {
        void Use();
    }

    public interface ISpawnable
    {
        Rigidbody2D Body { get; }
    }
}
