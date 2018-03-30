# Multi-tenant bot activity support

This project is an example of how to serve multiple bot tenants when it comes to utilizing each custoemr's settings e.g. LUIS or Microsoft App settings.

# IResolver interface

This interface is meant to abstract the data layer retaining bot settings. Bot settings can be retrieved from a database, json file, XML or any other entites e.g. 3rd party web apis. By injecting 