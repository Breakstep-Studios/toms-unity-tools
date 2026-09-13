# Toms Unity Tools
Tools library for Unity that includes tons of helper classes &amp; methods

# Quick Start
1. Install using package manager... update this
# Requirements

* TextMesh Pro (declared in `package.json`).

The alert service and its prefab have been removed. DOTween and DOTween Pro are no longer required by this package. Projects using `AlertService` must remove or replace those usages and any scene or prefab components that reference it.

# TODO: Package organization

This library currently bundles too many unrelated tools together and needs to be split into smaller, focused packages.

* [ ] Split the library into separate packages by responsibility, with each package declaring only the dependencies it needs.
* [ ] Consider bringing back the alert service as its own optional package, with its animation dependencies contained there.
* [ ] Rename the package to something appropriate for Breakstep, including its package identifier and display name.
