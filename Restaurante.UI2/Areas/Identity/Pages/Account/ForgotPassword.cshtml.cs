// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Net.Mail;

namespace Restaurante.UI2.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {

        private readonly UserManager<IdentityUser> _userManager;

        public ForgotPasswordModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            [Required(ErrorMessage = "El nombre es requerido")]
            [Display(Name = "Nombre")]
            public string UserName { get; set; }


            [Required(ErrorMessage = "La contraseña es requerida")]
            [StringLength(100, ErrorMessage = "El {0} debe tener al menos {2} y un máximo de {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Nueva Contraseña")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar la nueva contraseña")]
            [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByNameAsync(Input.UserName);
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                Input.Code = code;

                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    return RedirectToPage("./ResetPasswordConfirmation");
                }

                var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
                if (result.Succeeded)
                {
                    string elCorreoElectronicoDelUsuario = user.Email;

                    string elMensajeDeCambioDeContrasena = "Le informamos que el cambio de clave de la cuenta del usuario " + Input.UserName + " se ejecutó satisfactoriamente.";

                    string elAsuntoDelCorreo = "Cambio de clave";

                    string elCuerpoDelCorreo = "<body><text>" + elMensajeDeCambioDeContrasena + "</text></body>";


                    EnviarCorreo(elCorreoElectronicoDelUsuario, elAsuntoDelCorreo, elCuerpoDelCorreo);


                    return RedirectToPage("./ResetPasswordConfirmation"); ;
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return Page();
            }

            return Page();
        }

        public void EnviarCorreo(string destinatario, string asunto, string cuerpo)
        {

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.Credentials = new NetworkCredential("proyectolenguajes18@gmail.com", "qhqtwqpcfgdfflla");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;

            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("proyectolenguajes18@gmail.com", "proyectolenguaje");
            correo.To.Add(new MailAddress(destinatario));
            correo.Subject = asunto;
            correo.IsBodyHtml = true;
            correo.Body = cuerpo;

            smtp.Send(correo);

        }
    }
}
