namespace iSprite.ThirdControl.FarsiLibrary.Design
{
    using iSprite.ThirdControl.FarsiLibrary;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;

    public class FATabStripDesigner : ParentControlDesigner
    {
        private IComponentChangeService changeService;

        protected override void Dispose(bool disposing)
        {
            this.changeService.ComponentRemoving -= new ComponentEventHandler(this.OnRemoving);
            base.Dispose(disposing);
        }

        protected override bool GetHitTest(Point point)
        {
            HitTestResult result = this.Control.HitTest(point);
            if ((result != HitTestResult.CloseButton) && (result != HitTestResult.MenuGlyph))
            {
                return false;
            }
            return true;
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            this.changeService = (IComponentChangeService) this.GetService(typeof(IComponentChangeService));
            this.changeService.ComponentRemoving += new ComponentEventHandler(this.OnRemoving);
            this.Verbs.Add(new DesignerVerb("Add TabStrip", new EventHandler(this.OnAddTabStrip)));
            this.Verbs.Add(new DesignerVerb("Remove TabStrip", new EventHandler(this.OnRemoveTabStrip)));
        }

        private void OnAddTabStrip(object sender, EventArgs e)
        {
            IDesignerHost service = (IDesignerHost) this.GetService(typeof(IDesignerHost));
            DesignerTransaction transaction = service.CreateTransaction("Add TabStrip");
            FATabStripItem tabItem = (FATabStripItem) service.CreateComponent(typeof(FATabStripItem));
            this.changeService.OnComponentChanging(this.Control, null);
            this.Control.AddTab(tabItem);
            tabItem.Title = "TabStrip Page " + ((this.Control.Items.IndexOf(tabItem) + 1)).ToString();
            this.Control.SelectItem(tabItem);
            this.changeService.OnComponentChanged(this.Control, null, null, null);
            transaction.Commit();
        }

        private void OnRemoveTabStrip(object sender, EventArgs e)
        {
            DesignerTransaction transaction = ((IDesignerHost) this.GetService(typeof(IDesignerHost))).CreateTransaction("Remove Button");
            this.changeService.OnComponentChanging(this.Control, null);
            FATabStripItem tabItem = this.Control.Items[this.Control.Items.Count - 1];
            this.Control.UnSelectItem(tabItem);
            this.Control.Items.Remove(tabItem);
            this.changeService.OnComponentChanged(this.Control, null, null, null);
            transaction.Commit();
        }

        private void OnRemoving(object sender, ComponentEventArgs e)
        {
            IDesignerHost service = (IDesignerHost) this.GetService(typeof(IDesignerHost));
            if (e.Component is FATabStripItem)
            {
                FATabStripItem component = e.Component as FATabStripItem;
                if (this.Control.Items.Contains(component))
                {
                    this.changeService.OnComponentChanging(this.Control, null);
                    this.Control.RemoveTab(component);
                    this.changeService.OnComponentChanged(this.Control, null, null, null);
                    return;
                }
            }
            if (e.Component is FATabStrip)
            {
                for (int i = this.Control.Items.Count - 1; i >= 0; i--)
                {
                    FATabStripItem tabItem = this.Control.Items[i];
                    this.changeService.OnComponentChanging(this.Control, null);
                    this.Control.RemoveTab(tabItem);
                    service.DestroyComponent(tabItem);
                    this.changeService.OnComponentChanged(this.Control, null, null, null);
                }
            }
        }

        protected override void PreFilterProperties(IDictionary properties)
        {
            base.PreFilterProperties(properties);
            properties.Remove("DockPadding");
            properties.Remove("DrawGrid");
            properties.Remove("Margin");
            properties.Remove("Padding");
            properties.Remove("BorderStyle");
            properties.Remove("ForeColor");
            properties.Remove("BackColor");
            properties.Remove("BackgroundImage");
            properties.Remove("BackgroundImageLayout");
            properties.Remove("GridSize");
            properties.Remove("ImeMode");
        }

        protected override void WndProc(ref Message msg)
        {
            if (msg.Msg == 0x201)
            {
                Point pt = this.Control.PointToClient(Cursor.Position);
                FATabStripItem tabItemByPoint = this.Control.GetTabItemByPoint(pt);
                if (tabItemByPoint != null)
                {
                    this.Control.SelectedItem = tabItemByPoint;
                    ArrayList components = new ArrayList();
                    components.Add(tabItemByPoint);
                    ((ISelectionService) this.GetService(typeof(ISelectionService))).SetSelectedComponents(components);
                }
            }
            base.WndProc(ref msg);
        }

        public override ICollection AssociatedComponents
        {
            get
            {
                return this.Control.Items;
            }
        }

        public new  FATabStrip Control
        {
            get
            {
                return (base.Control as FATabStrip);
            }
        }
    }
}
