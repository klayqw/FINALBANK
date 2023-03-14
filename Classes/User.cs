using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINALBANK.Classes;

public class User : INotifyPropertyChanged
{

    private string email;
    public string Email
    {
        get => email;
        set
        {
            Console.WriteLine(value.EndsWith("@gmail.com"));
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
                throw new Exception("Value too small or too big");
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
                throw new Exception("Value too small or too big");
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
                throw new Exception("Value too small or too big");
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
                throw new Exception("Value too small or too big");
            }
            lastname = value;
            OnPropertyChanged(nameof(Lastname));
        }
    }
    private string? lastname;
    public string? Creditcard
    {
        get => creditcard;
        set
        {
            if (value == null)
            {
                creditcard = null;
            }
            if (value.Length != 16)
            {
                throw new Exception("Value too small or too big");
            }
            creditcard = value;
            OnPropertyChanged(nameof(Creditcard));
        }
    }

    private string? creditcard;
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
    public int? Cvv
    {
        get => cvv;
        set
        {
            if (value == null)
            {
                cvv = null;
            }
            cvv = value;
            OnPropertyChanged(nameof(Cvv));
        }
    }
    private int? cvv;
    public DateTime? Creditcarddate
    {
        get => creditcarddate;
        set
        {
            if (value == null)
            {
                creditcarddate = null;
            }
            if (value.Value.Year < DateTime.Now.Year || value.Value.Year > DateTime.Now.Year + 7)
            {
                throw new Exception("Value too small or too big");
            }
            creditcarddate = value;
            OnPropertyChanged(nameof(Creditcarddate));
        }
    }
    private DateTime? creditcarddate;
    public DateTime? Birthday
    {
        get => birthday;
        set
        {
            if (value == null)
            {
                birthday = null;
            }
            if (value.Value.Year < 1970)
            {
                throw new Exception("Value null");
            }
            birthday = value;
            OnPropertyChanged(nameof(Birthday));
        }
    }
    private DateTime? birthday;
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
                throw new Exception("Value too small or too big");
            }
            phone = value;
            OnPropertyChanged(nameof(Phone));
        }
    }
    private string? phone;
    public double? Balance
    {

        get => balance;
        set
        {
            if (value == null)
            {
                balance = null;
            }
            if (value >= double.MaxValue)
            {
                throw new Exception("Value too small or too big");
            }
            balance = value;
            OnPropertyChanged(nameof(Balance));
        }
    }
    public double? balance;

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
