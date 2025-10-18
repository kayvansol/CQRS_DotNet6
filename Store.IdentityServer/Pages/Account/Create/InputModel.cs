// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

namespace Store.IdentityServer.Pages.Create;

public class InputModel
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }

    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Mobile { get; set; }

    public string? ReturnUrl { get; set; }

    public string? Button { get; set; }
}