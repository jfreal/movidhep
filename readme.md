# This is an old project from 2014/2015 #

I left the company and moved on to other things.  The people who took the company over ended up shutting the company down.  I asked the old owners for the code for nostalgia purposes.  I loved this project.  Although nothing worked out like I thought it would, the journey was the real reward.

# General #

Movid HEP is a SAAS product sold to physical therapy clinics for a monthly fee.

The primary purpose of Movid is to allow physical therapists to send their patients a collection
of videos, demonstrating exercises to perform at home.  This is sort of similar to how a doctor
prescribes medication. Think of the videos as medication and the doctors are
physical therapists.

**For a good overview of the core feature of Movid please check out this video**
http://vimeo.com/76149049

Feel free to signup for a trial account and check things out.  If you find bugs please report them and you
can fix them later. ;)

## Technology ##

In the Movid solution there are three projects.

**Movid.Shared** - Some shared code used in both projects.

**Movid.Marketing** - A public site running at www.movidhep.com, deployed on a Azure website.

**Movid.App** - A private site running at app.movidhep.com, deployed on a separate Azure website.

All projects are Asp.net 4.5, C# 4.0 using the latest versions of MVC, Web API.

Front end is some Knockout but we are moving towards Angular.js.

Back end is RavenDb (http://ravendb.net/) primarily used for RavenDb's killer search features.  RavenDb takes a little
getting used to but is pretty awesome once you understand its quirks.  The database is hosted on ravenhq.com


## Infrastructure ##

There are 3 branches, dev, test and master. **Always do work in the dev branch.**  The master branch is what we currectly use in production and shouldn't be used unless we are doing live production fixes.

There are three Azure websites which are setup to continuously integrate with any committed changes to Movid.App.
- http://movidapp-dev.azurewebsites.net/ - dev branch - debug project configuration
- http://movidapp-test.azurewebsites.net/ - test branch
- https://app.movidhep.com/ - master branch, do not use

Movid.Marketing isn't setup with continuous integration and simply gets deployed on demand.

We have 3 RavenDB instances for each of the three environments as well.  **You should never be using the main movid database.**  You could run a local instance of the database as well by downloading a 2.5 server from http://ravendb.net/
- movid-dev - dev branch - debug project configuration
- movid-test - test branch
- movid - production, do not use!

The correct connection strings are in the web.config transforms.

## Points of Interest ##

* The site has evolved from a fairly simple MVP (minimal viable product) to where it is now.  There are some warts
and ugly parts.  I did my best to keep things clean but just haven't had the time.

* The site is multi tenant, meaning multiple users share the same database.  You may see calls to Ownership.Assign()
to give things to the right people.  Just be aware of this when you're working on the site.

* All exercises are copied for each account.   This is a bit different
from most systems that share the same data so I'm bringing attention to that point here.
