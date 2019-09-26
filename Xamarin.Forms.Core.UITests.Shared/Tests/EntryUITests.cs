using NUnit.Framework;
using Xamarin.Forms.Controls.Issues;
using Xamarin.Forms.CustomAttributes;
using Xamarin.UITest.Queries;

namespace Xamarin.Forms.Core.UITests
{

	[TestFixture]
	[Category(UITestCategories.Entry)]
	internal class EntryUITests : _ViewUITests
	{
		public EntryUITests()
		{
			PlatformViewType = Views.Entry;
		}

		protected override void NavigateToGallery()
		{
			App.NavigateToGallery(GalleryQueries.EntryGallery);
		}

		[Test]
		[UiTest(typeof(Entry), "Focus")]
		public override void _Focus()
		{
			var remote = new StateViewContainerRemote(App, Test.VisualElement.Focus, PlatformViewType);
			remote.GoTo();
			
			Assert.IsFalse(IsFocused());
			remote.TapView();
			Assert.IsTrue(IsFocused());
			App.Tap("Go"); // Won't do anything, we just need to take focus away from the Entry
			Assert.IsFalse(IsFocused());
		}

		bool IsFocused()
		{
			var focusedText = App.Query(q => q.Marked("FocusStateLabel").All())[0].ReadText();
			return System.Convert.ToBoolean(focusedText);
		}

		[UiTestExempt(ExemptReason.CannotTest, "Invalid interaction")]
		public override void _GestureRecognizers()
		{
		}

		public override void _IsFocused()
		{
		}

		// TODO
		public override void _UnFocus()
		{
		}


		
		// TODO
		// Implement control specific ui tests
		[Test]
		[UiTest(typeof(Entry), "Completed")]
		[Category(UITestCategories.UwpIgnore)]
		public virtual void Completed()
		{
			var remote = new EventViewContainerRemote(App, Test.Entry.Completed, PlatformViewType);
			remote.GoTo();

			App.EnterText(q => q.Raw(remote.ViewQuery), "Test");

#if !__TIZEN__
			App.PressEnter();

			var eventLabelText = remote.GetEventLabel().Text;
			Assert.AreEqual(eventLabelText, "Event: Completed (fired 1)");
#endif
		}

		[Test]
		[UiTest(typeof(Entry), "ClearButtonVisibility")]
		[Category(UITestCategories.ManualReview)]
		public void ClearButtonVisibility()
		{
			var remote = new StateViewContainerRemote(App, Test.Entry.ClearButtonVisibility, PlatformViewType);
			remote.GoTo();

			App.WaitForElement(q => q.Marked("Toggle ClearButtonVisibility"));
			App.Tap(q => q.Marked("Toggle ClearButtonVisibility"));
		}

		protected override void FixtureTeardown()
		{
			App.NavigateBack();
			base.FixtureTeardown();
		}
	}
}