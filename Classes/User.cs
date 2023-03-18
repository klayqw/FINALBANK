using System;
using System.ComponentModel;
using System.Linq;

namespace FINALBANK.Classes;

public class User : INotifyPropertyChanged
{
    public Card? card { get; set; }


    private string email;
    public string Email
    {
        get => email;
        set
        {           
            if(value.EndsWith("@gmail.com"))
            {
                email = value;
                return;
            }
            else if (value.EndsWith("@mail.ru") )
            {
                email = value;
                return;
            }
            else if (value.EndsWith("@outlook.com"))
            {
                email = value;
                
                return;
            }
            throw new Exception("Wrong Email");
            OnPropertyChanged(nameof(Email));
        }
    }

    public string? Nickname
    {
        get => nickname;
        set
        {
            if (value == null)
            {
                nickname = null;
            }
            if (value.Length <= 0 || value.Length >= 12)
            {
                throw new Exception("Value name too small or too big");
            }
            nickname = value;
            OnPropertyChanged(nameof(Nickname));
        }
    }
    private string? nickname;

    public string? Password
    {
        get => password;
        set
        {
            if (value == null)
            {
                password = null;
            }
            if (value.Length <= 0 || value.Length < 4 || value.Length >= 16)
            {
                throw new Exception("Value pass too small or too big");
            }
            password = value;
            OnPropertyChanged(nameof(Password));
        }

    }
    private string? password;

    public string? Firstname
    {
        get => firstname;
        set
        {
            if (value == null)
            {
                firstname = null;
            }
            if (value.Length <= 0 || value.Length < 3 || value.Length >= 16)
            {
                throw new Exception("Value first name too small or too big");
            }
            firstname = value;
            OnPropertyChanged(nameof(Firstname));
        }
    }
    private string? firstname;
    public string? Lastname
    {
        get => lastname;

        set
        {
            if (value == null)
            {
                lastname = null;
            }
            if (value.Length <= 0 || value.Length < 6 || value.Length >= 18)
            {
                throw new Exception("Value last name too small or too big");
            }
            lastname = value;
            OnPropertyChanged(nameof(Lastname));
        }
    }
    private string? lastname;
    
    public int? Id
    {
        get => id;
        set
        {
            id = value;
            OnPropertyChanged(nameof(Id));
        }
    }
    private int? id;
   
    
    public string? Phone
    {
        get => phone;
        set
        {
            if (value == null)
            {
                phone = null;
            }
            if (value.Length <= 0 || value.Length >= 12)
            {
                throw new Exception("Value phone too small or too big");
            }                       
            phone = value;
            OnPropertyChanged(nameof(Phone));
        }
    }
    private string? phone;   

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        var handler = PropertyChanged;
        if (handler != null)
            handler(this, new PropertyChangedEventArgs(propertyName));

        // With C# 6 this can be replaced with
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
