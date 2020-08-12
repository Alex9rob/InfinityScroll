// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace InfiniteScroll.Collection
{
    [Register ("Cell")]
    partial class Cell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel _lblTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (_lblTitle != null) {
                _lblTitle.Dispose ();
                _lblTitle = null;
            }
        }
    }
}