using Resources.PortalResources;

namespace Notifications.Core.Templates;

public static class StandardEmailTemplate
{

    public static string AutomatedEmailHtmlTemplate (string title, string content, string appName) => @$"
<!DOCTYPE html>
<html lang=""en"">
<head>
<meta charset=""UTF-8"">
<title>{title}</title>
<meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
<style>
body{{
    margin:0;
    padding:0;
    background:#f4f6f8;
    font-family: Arial, Helvetica, sans-serif;
}}
.container{{
    max-width:600px;
    margin:40px auto;
    background:#ffffff;
    border-radius:8px;
    overflow:hidden;
    box-shadow:0 2px 8px rgba(0,0,0,0.05);
}}
.header{{
    background:#4f46e5;
    color:white;
    padding:20px;
    text-align:center;
    font-size:22px;
    font-weight:bold;
}}
.content{{
    padding:30px;
    color:#333;
    line-height:1.6;
}}
.button{{
    display:inline-block;
    margin-top:20px;
    padding:12px 20px;
    background:#4f46e5;
    color:#ffffff;
    text-decoration:none;
    border-radius:5px;
    font-size:14px;
}}
.footer{{
    background:#f1f1f1;
    text-align:center;
    padding:15px;
    font-size:12px;
    color:#777;
}}
</style>
</head>

<body>

<div class=""container"">

<div class=""header"">
{title}
</div>

<div class=""content"">

<p>{content}</p>

</div>

<div class=""footer"">
© {DateTime.Now.Year} {appName} • {PortalResources.cAutomatedEmail}
</div>
</div>

</body>
</html>";
}