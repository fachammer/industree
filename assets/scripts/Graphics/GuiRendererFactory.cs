using Industree.Graphics.Internal;
namespace Industree.Graphics
{
    public static class GuiRendererFactory
    {
        public static IGuiRenderer GetResolutionIndependentRenderer()
        {
            IScreen screen = UnityScreen.GetInstance();
            return new ResolutionIndependentGuiRenderer(screen, GetResolutionDependentRenderer());
        }

        private static IGuiRenderer GetResolutionDependentRenderer()
        {
            return new UnityGuiRenderer();
        }
    }
}
