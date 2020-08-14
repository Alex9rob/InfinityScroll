// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace InfiniteScroll
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton _btnNet { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        InfiniteScroll.Collection.InfiniteCollectionView _collectionView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (_btnNet != null) {
                _btnNet.Dispose ();
                _btnNet = null;
            }

            if (_collectionView != null) {
                _collectionView.Dispose ();
                _collectionView = null;
            }
        }
    }
}