using System;
using System.Collections.Generic;

// Define an enum for unit types
public enum UnitType
{
    Warrior,
    Mage,
    Archer,
    Healer
}

// Define an enum for item types
public enum ItemType
{
    Weapon,
    Armor,
    Accessory
}

// Define a class for items
public class Item
{
    public string Name { get; set; }
    public int BonusHealth { get; set; }
    public int BonusAttack { get; set; }
    public int BonusDefense { get; set; }

    public Item(string name, int bonusHealth, int bonusAttack, int bonusDefense)
    {
        Name = name;
        BonusHealth = bonusHealth;
        BonusAttack = bonusAttack;
        BonusDefense = bonusDefense;
    }
}

// Define a class for units
public class Unit
{
    public string Name { get; set; }
    public UnitType Type { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public List<Item> Inventory { get; set; }

    public Unit(string name, UnitType type, int health, int attack, int defense)
    {
        Name = name;
        Type = type;
        Health = health;
        Attack = attack;
        Defense = defense;
        Inventory = new List<Item>();
    }

    public void EquipItem(Item item)
    {
        Inventory.Add(item);
        Health += item.BonusHealth;
        Attack += item.BonusAttack;
        Defense += item.BonusDefense;
    }

    public void AttackUnit(Unit target)
    {
        int damageDealt = Math.Max(0, Attack - target.Defense);
        target.Health -= damageDealt;
        Console.WriteLine($"{Name} attacks {target.Name} for {damageDealt} damage.");
    }
}

// Define a class for the game board
public class Board
{
    public List<Unit> units = new List<Unit>();

    public void AddUnit(Unit unit)
    {
        units.Add(unit);
    }

    public void Battle()
    {
        foreach (var unit in units)
        {
            // For simplicity, let's assume each unit attacks the first unit in the list
            if (units.Count > 1)
                unit.AttackUnit(units[1]);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create some units
        Unit warrior = new Unit("Warrior", UnitType.Warrior, 100, 20, 10);
        Unit mage = new Unit("Mage", UnitType.Mage, 80, 30, 5);
        Unit archer = new Unit("Archer", UnitType.Archer, 90, 25, 8);

        // Create some items
        Item sword = new Item("Sword", 0, 10, 0);
        Item staff = new Item("Staff", 0, 15, 0);
        Item bow = new Item("Bow", 0, 12, 0);

        // Equip items to units
        warrior.EquipItem(sword);
        mage.EquipItem(staff);
        archer.EquipItem(bow);

        // Create a board and add units
        Board board = new Board();
        board.AddUnit(warrior);
        board.AddUnit(mage);
        board.AddUnit(archer);

        // Simulate a battle
        board.Battle();

        // Print unit health after battle
        foreach (var unit in board.units)
        {
            Console.WriteLine($"{unit.Name} ({unit.Type}) has {unit.Health} health left.");
        }
    }
}
