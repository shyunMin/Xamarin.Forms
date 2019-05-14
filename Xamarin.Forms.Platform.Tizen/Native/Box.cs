using System;
using ElmSharp;
using EBox = ElmSharp.Box;

#if __MATERIAL__
using Tizen.NET.MaterialComponents;
#endif

namespace Xamarin.Forms.Platform.Tizen.Native
{
#if __MATERIAL__
	/// <summary>
	/// Extends the MCard class with functionality useful to Xamarin.Forms renderer.
	/// </summary>
	/// <remarks>
	/// This class overrides the layout mechanism. Instead of using the native layout,
	/// <c>LayoutUpdated</c> event is sent.
	/// </remarks>
	public class MaterialBox : MCard
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Xamarin.Forms.Platform.Tizen.Native.MaterialBox"/> class.
		/// </summary>
		/// <param name="parent">The parent EvasObject.</param>
		public MaterialBox(EvasObject parent) : base(parent)
		{
			SetLayoutCallback(() => { NotifyOnLayout(); });
		}
#else
	/// <summary>
	/// Extends the ElmSharp.Box class with functionality useful to Xamarin.Forms renderer.
	/// </summary>
	/// <remarks>
	/// This class overrides the layout mechanism. Instead of using the native layout,
	/// <c>LayoutUpdated</c> event is sent.
	/// </remarks>
	public class Box : EBox
	{
		public Box(EvasObject parent) : base(parent)
		{
			SetLayoutCallback(() => { NotifyOnLayout(); });
		}		
#endif

		/// <summary>
		/// The last processed geometry of the Box which was reported from the native layer.
		/// </summary>
		Rect _previousGeometry;

		/// <summary>
		/// Notifies that the layout has been updated.
		/// </summary>
#if __MATERIAL__
		public new event EventHandler<LayoutEventArgs> LayoutUpdated;
#else
		public event EventHandler<LayoutEventArgs> LayoutUpdated;
#endif

		/// <summary>
		/// Triggers the <c>LayoutUpdated</c> event.
		/// </summary>
		/// <remarks>
		/// This method is called whenever there is a possibility that the size and/or position has been changed.
		/// </remarks>
		void NotifyOnLayout()
		{
			if (null != LayoutUpdated)
			{
				var g = Geometry;

				if (0 == g.Width || 0 == g.Height || g == _previousGeometry)
				{
					// ignore irrelevant dimensions
					return;
				}

				LayoutUpdated(this, new LayoutEventArgs()
					{
						Geometry = g,
					}
				);

				_previousGeometry = g;
			}
		}
	}
}
