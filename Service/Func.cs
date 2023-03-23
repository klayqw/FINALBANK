using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FINALBANK.Models;

namespace FINALBANK.Service;

class Func
{
    public static Settings OpenSettings()
    {
        var json = File.ReadAllText("settings.json");
        var settings = JsonSerializer.Deserialize<Settings>(json);
        return settings;
    }
    public static int SendCodeEmail(User user)
    {
        int code = new Random().Next(10000, 99999);

        string fromMail = "pudgearcane5@gmail.com";
        string fromPassword = "pwauepbjjpoovets";

        var strComputerName = "UNIVERSALBANK";

        MailMessage message = new MailMessage();
        message.From = new MailAddress(fromMail);
        message.Subject = strComputerName;
        message.To.Add(new MailAddress(user.Email));
        message.Body = "<html><body>";
        message.Body += code;
        message.Body += "<br>";
        message.Body += "Have a good day!";
        message.IsBodyHtml = true;

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(fromMail, fromPassword),
            EnableSsl = true,
        };

        smtpClient.Send(message);

        return code;
    }
    public User UserUpdate(User user)
    {
        var json = File.ReadAllText("User Base/USERBASE.json");
        var list = JsonSerializer.Deserialize<List<User>>(json);
        return user = list.Find(x => x.Id == user.Id);
    }

    public static void LoadUserInFile(List<User> user)
    {
        var json = JsonSerializer.Serialize(user);
        File.WriteAllText("User Base/USERBASE.json", json);
        return;
    }

    public static List<User> GetUsers()
    {
        var json = File.ReadAllText("User Base/USERBASE.json");
        var list = JsonSerializer.Deserialize<List<User>>(json);
        return list;
    }
}
