// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

namespace Store.IdentityServer.Pages.Account.TwoFactor;

public class InputModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string TwoFactor { get; set; }

    public bool RememberLogin { get; set; }

    public string ReturnUrl { get; set; }

    public string Button { get; set; }
}