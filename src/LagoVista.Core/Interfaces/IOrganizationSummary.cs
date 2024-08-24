﻿using LagoVista.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.Core.Interfaces
{
    public interface IOrganizationSummary
    {
        string Id { get; set; }
        string Text { get; set; }
        string Name { get; set; }
        string Namespace { get; set; }
        EntityHeader Logo { get; set; }
        string Icon { get; set; }
        string TagLine { get; set; }
        string DefaultTheme { get; set; }

        EntityHeader ToEntityHeader();
    }
}
