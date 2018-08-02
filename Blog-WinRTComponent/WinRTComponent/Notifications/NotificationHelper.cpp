#include "pch.h"
#include "NotificationHelper.h"

using namespace Notifications;
using namespace Platform;
using namespace Windows::Data::Xml::Dom;
using namespace Windows::UI::Notifications;

NotificationHelper::NotificationHelper()
{
}

void NotificationHelper::ShowNotification(Platform::String^ title, Platform::String^ content)
{
	Platform::String^ xml = "<toast><visual><binding template='ToastGeneric'><text>" + title +" </text><text>" + content +"</text></binding></visual></toast>";
	XmlDocument^ doc = ref new XmlDocument();
	doc->LoadXml(xml);


	ToastNotification^ toast = ref new ToastNotification(doc);
	ToastNotificationManager::CreateToastNotifier()->Show(toast);
}