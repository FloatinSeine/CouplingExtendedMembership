Coupling - ExtendedMembershipProvider
===========

A basic working PoC to demonstrate using the ExtendedMembershipProvider with a DDD membership account model data management and <a href="http://ravendb.net">RaveDB</a> storage.
The aim was the decouple the storage engine from the main application and Membership Provider.<br />
<br />
Its a bit rough and ready at the moment, much is missing but most of the basic concepts are included providing a base to extend further.</br>
<br />
<br />
The design makes it easy enough now to plug in a different storage engine, e.g. MySQL or even an API and to switch out the ExtendedMembershipProvider in favour of different authentication method such as using Claims framework
<br/>
<br />
<strong>Configuration</strong><br />
Modify the web.config of the web application to inlcude the following sections. <br />
The key property to modify is the connectionString to ensure points to your instance of RaveDB.
<pre>
&lt;configuration&gt;
  &lt;connectionStrings&gt;
    &lt;add name="CouplingDataStore" connectionString="URL=http://localhost:8080;Database=Coupling" /&gt;
  &lt;/connectionStrings&gt;

  &lt;system.web&gt;
    &lt;membership defaultProvider="CouplingMembershipProvider" userIsOnlineTimeWindow="20"&gt;
      &lt;providers&gt;
        &lt;clear /&gt;
        &lt;add name="CouplingMembershipProvider" type="Coupling.Web.ApplicationServices.Memberships.CouplingExtendedMembershipProvider, Coupling.Web.ApplicationServices, Version=1.0.0.0, Culture=neutral" connectionStringName="CouplingDataStore"
             applicationName="Coupling" requiresQuestionAndAnswer="false" enablePasswordReset="true" enablePasswordRetrieval="false" /&gt;
      &lt;/providers&gt;
    &lt;/membership&gt;
  &lt;/system.web&gt;
&lt;/configuration&gt;
</pre>


<strong>Web Application Integration</strong><br />
To integration into your own web application see the sample Coupling.Web package.<br>
Add a reference to the Coupling.Web.ApplicationServices package and include Structuremap which has been used for Dependency Injection. Each package contains a DepenencyResolution folder with a registry for injectable components.


