﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Text;
using System.Threading.Tasks;
using CestFurDelivery.WebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace CestFurDelivery.WebApp.Areas.Identity.Pages.Account
{
    public class ConfirmEmailChangeModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ConfirmEmailChangeModel> _logger;

        public ConfirmEmailChangeModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<ConfirmEmailChangeModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string email, string code)
        {
            if (userId == null || email == null || code == null)
            {
                _logger.LogInformation($"{DateTime.Now} - ConfirmEmailChange - {User.Identity.Name} - No data for confirm email");
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogInformation($"{DateTime.Now} - ConfirmEmailChange - {User.Identity.Name} - Unable to load user with ID <{userId}>");
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ChangeEmailAsync(user, email, code);
            if (!result.Succeeded)
            {
                _logger.LogInformation($"{DateTime.Now} - ConfirmEmailChange - {User.Identity.Name} - Error changing email");
                StatusMessage = "Error changing email.";
                return Page();
            }

            // In our UI email and user name are one and the same, so when we update the email
            // we need to update the user name.
            var setUserNameResult = await _userManager.SetUserNameAsync(user, email);
            if (!setUserNameResult.Succeeded)
            {
                _logger.LogInformation($"{DateTime.Now} - ConfirmEmailChange - {User.Identity.Name} - Error changing user name");
                StatusMessage = "Error changing user name.";
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation($"{DateTime.Now} - ConfirmEmailChange - {User.Identity.Name} - Email changed");
            StatusMessage = "Thank you for confirming your email change.";
            return Page();
        }
    }
}
