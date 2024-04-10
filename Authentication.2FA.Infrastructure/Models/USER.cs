using System;
using System.Collections.Generic;

namespace Authentication._2FA.Infrastructure.Models;

public partial class USER
{
    public int ID_USER { get; set; }

    public string DS_NAME { get; set; } = null!;

    public string DS_EMAIL { get; set; } = null!;

    public string DS_PASSWORD { get; set; } = null!;

    public DateTime? LAST_VALIDATION { get; set; }

    public DateTime CREATED_AT { get; set; }

    public DateTime? UPDATED_AT { get; set; }

    public DateTime? DELETED_AT { get; set; }
}
