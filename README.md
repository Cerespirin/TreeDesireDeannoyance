# Tree Desire Deannoyance
Makes playing tree-huggers less tedious by adding two features that automate its most common tasks.
## Features
### Always Extract Trees Toggle
While enabled you will not need to manually designate trees for extracting just to have your constructors and growers blatantly disregard your orders. Trees will *always* be extracted rather than cut unless *specifically* designated otherwise.
### Replanting Zones
Designate configurable zones where extracted trees will be automatically ordered for replanting. Replanting zones also have some growing zone functionality so if you're not a tree-lover or you have mods that add additional extractables you can set those plants to be harvested or obstructing plants to be cut automatically. By default, all trees relevant to tree-lovers will be replanted but not "fake" trees like saguaros or timbershrooms.
## Foraging Areas (COMING SOON)
Designate an area that can be configured to automatically process certain types of plants. Plants can be designated for cutting, harvesting, or extracting (if possible). By default, this will designate stumps to be cut, giving a small trickle of 100% tree-lover approved wood.
## Balance
This mod does nothing that you could not do manually, but may make the game slightly easier as you will no longer have to deal with unnecessary mood penalties from needlessly cutting trees.
## Technical
While this mod may seem simple, intercepting any possible call to cut a tree required some very aggressive patching. Most notably, it patches `JobMaker.MakeJob(JobDef, LocalTargetInfo)` and the `Job(JobDef, LocalTargetInfo)` constructor itself which should make it impossible even for other mods to cut trees while the *Always Extract Trees* toggle is enabled.
