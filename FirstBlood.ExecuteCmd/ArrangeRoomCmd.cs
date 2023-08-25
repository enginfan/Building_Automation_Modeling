using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Windows;
using FirstBlood.Arrangement;
using FirstBlood.Builder;
using FirstBlood.Transform;
using FirstBlood.View.Model;
using FirstBlood.View.View;
using System;
using System.Windows.Interop;
using ARoom = Autodesk.Revit.DB.Architecture.Room;
using FRoom = FirstBlood.Core.Room;

namespace FirstBlood.ExecuteCmd
{
    [Transaction(TransactionMode.Manual)]
    public class ArrangeRoomCmd : IExternalCommand
    {
        private ExternalEvent RoomCreationEvent;

        public static RoomEventArgs Args = new RoomEventArgs();

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                var uiDoc = commandData.Application.ActiveUIDocument;
                var doc = uiDoc.Document;
                var configWindow = RoomTypeConfig.Instance;
                var mainhelper = new WindowInteropHelper(configWindow);
                mainhelper.Owner = ComponentManager.ApplicationWindow;
                configWindow.Show();
                configWindow.Create += CreateRoom;
                RoomCreationEvent = ExternalEvent.Create(new RoomCreation(doc, message));
                return Result.Succeeded;
            }
            catch (Exception e)
            {
                return Result.Failed;
            }

        }
        private void CreateRoom(object sender, RoomEventArgs e)
        {
            Args = e;
            RoomCreationEvent.Raise();
        }
    }

    public class RoomCreation : IExternalEventHandler
    {
        private Document Doc;

        private string myMessage;
        public RoomCreation(Document doc, string message)
        {
            Doc = doc;
            myMessage = message;
        }
        public void Execute(UIApplication app)
        {
            try
            {
                var uiDoc = app.ActiveUIDocument;
                while (true)
                {
                    try
                    {
                        var roomRef = uiDoc.Selection.PickObject(ObjectType.Element, new RoomSelFilter());
                        var aRoom = Doc.GetElement(roomRef.ElementId) as ARoom;
                        var roomExtractor = new RoomExtractor();
                        var dir = Autodesk.Revit.DB.Transform.CreateRotation(XYZ.BasisZ, ArrangeRoomCmd.Args.Angle * Math.PI / 180).OfVector(XYZ.BasisY);
                        var bRoom = roomExtractor.Create(aRoom, dir);
                        var roomArranger = new RoomArranger(ArrangeRoomCmd.Args.Role, ArrangeRoomCmd.Args.Type);
                        roomArranger.Arrange(bRoom);
                        var tranB = new Transaction(Doc);
                        tranB.Start("CreteRoom");
                        var roomBuilder = new RoomBuilder(Doc);
                        roomBuilder.Build(bRoom);
                        tranB.Commit();
                    }
                    catch (Autodesk.Revit.Exceptions.OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception e)
                    {
                        Autodesk.Revit.UI.TaskDialog.Show("错误", e.Message);
                        break;
                    }
                }
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public string GetName()
        {
            return "RoomCreation";
        }
    }
    public class RoomSelFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            return elem?.Category?.Id?.IntegerValue == (int)BuiltInCategory.OST_Rooms;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }

    }
}
