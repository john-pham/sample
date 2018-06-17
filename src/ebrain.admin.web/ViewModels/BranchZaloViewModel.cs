// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Ebrain.ViewModels
{
    public class BranchZaloViewModel
    {
        public Guid ZaloSMSId { get; set; }
        public Guid BranchId { get; set; }
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public string Code { get; set; } = "4Cj-Im5nxnO1_N4kHNJ9C7_9IrynNjHEEym4TI5ya1a4-XLsCNQBH0BGCou4Kxn3IyiZKNHAusfny7q5KttiRLJtTba7HCO7Fj5-LN56-aDtoHLWJqQBJtxt9rmKCEKm2-rxMnTeu2ezeIHkO7UDLJYQ2ZaP2ujSF9OmGtaUkZqpdJLDRdpl7J2uOcD4IgnAUi0k8r5EimCAkKe99q6zV7gDVZSj3ED9MDuhFMaYyX43yZm-HdcZK5h_Ap0HLgye5DScKcGFysRLXK8vy4te2OVHNHUzPEvknyUFSjCzTM_5XCa95HruLn2WyWOZIs0TDQp-41PLScKElO0nCLHsB7V8WbOWK7q8MIO7aiD70ddP5W";
        public string AppId { get; set; } = "724072444186981861";
        public string Secret { get; set; } = "QHO6VNY5BLCk4MY5uDS2";
        public string CallBackUrl { get; set; } = "http://superbrain.vn";
        public string HomeUrl { get; set; } = "http://superbrain.vn";
    }
}
