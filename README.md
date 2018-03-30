# Multi-tenant bot activity support

This project is an example for serving multiple tenants by a single bot activity action. The key area is supplying right Microsoft app settings (or e.g. LUIS  settings) to each activity submitted
via POST requests.

## IResolver interface

This interface is meant to abstract the data layer retaining bot settings like LUIS settings or Microsoft app settings. Such settings can be retrieved from a database, json file, XML or any other entities like a 
3rd party web api. 

As an example, we assumed Activity.From.Id is the tenant's / customer's key and passed it to Get() method of this interface to obtain the tenant's / customer's settings.