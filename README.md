# Planetary Planter


--Git Standards--

Instructions here include copy and paste code for commandline but feel free to use whatever git interface you prefer.

Basic Etiquette
Since the entire team is working in a repository, all members must be mindful of each other when pushing changes. Git and command line seem scary at first, but once you get the hang of it it’s not that bad.

Communication is very important when using Git as a team. Make sure that you let the team know when you are merging branches, and speak up if you run into a merge conflict. This will help in not overwriting someone else’s work. If you feel that someone else’s work should be changed, make sure that you ask them first. Do not change other people’s code without asking.

Overview of Branches

	Master Branch - Branch meant for the finished/polished product. DO NOT WORK IN THE MASTER BRANCH!

	Dev Branch - Work in progress branch. This branch is where all of the unstable but working code and assets go.
		     Artists can push assets to this branch without issue or needing to worry about merging branches.
		     Designers and programmers should not touch code while in this branch unless merging.

	Feature Branches - Branches created off of the dev branch that are meant for specific features.
			   	A Feature Branch should relate to a user story.

			   	These branches are mostly for the programmers or designers to mess around with game features.

			   	Keep pushing to these branches while a feature is still in progress,
			   		and make sure that you notify the other programmers/designers in Discord when you are
			   		going to merge a branch.

			   	Notifying others when you merge will help to avoid merge conflicts.
			   		It also lets everyone else on the team know what you are working on,
					and what kind of progress has been made.
					
				You should merge dev into your feature branch every time you start working,
					and should merge your feature branch into dev any time that your code 
					is in a working (not throwing errors) state.


----------PIPELINE----------

## Updating Your Branch

**Before doing anything on any branch or when starting a new work session, make sure that your working tree is clear and your branch is up to date.**

	Follow these commmandline steps to update your current branch:

	1. git status (Checks for any changes on your local machine)
	2. git fetch (Checks for any changes in remote repository)
	3. git pull (If fetch shows changes, pull the changes to your local machine)
	
	To get changes from dev when you start working in your current branch:
	4. git checkout dev
	5. git pull
	6. git checkout <your branch name>
	7. git merge dev
	8. resolve any merge conflicts



## General Workflow

**Important note: You do not need to include <> in any of the following commands, they are just being used to differentiate the command from a file/folder name**

This is the workflow that everyone will follow while on their own branch.

	1. git status (Checks for any changes on your local machine)
	2. git add <file/folder name> (Stages files for commit)
 		Optional add commands:
			git add -u (Stages all modified files for commit, will not stage newly created files)
			git add <folder name> (Stages everything in that folder)

	3. git commit -m 'message' (Commits files to be pushed, add a commit message so you know what was worked on)

		Example commit messages:
	'Adding README.txt'
	'Adding move function to player.cs'
	'Adding playerSprite.png to Assets folder'

	4. git push (Pushes files to the remote repository ON YOUR CURRENT BRANCH)


## Branch Management Workflow

### Branch Naming Conventions:

	All feature branches should follow this naming convention: feature_FeatureName

### Example of creating a feature branch for player movement:

	git checkout -b feature_PlayerMovement

### Creating a new branch

	git checkout -b <branch name>    (Creates a new branch and switches to it)
	git push --set-upstream origin <branch name> (Pushes branch to the remote repository)

### Deleting a branch

	git branch -d <branch name>

### Swapping to a different branch

**NB: Do not switch branches or merge while you have the editor open**

	git checkout <branch name>

### Merging branches

**If you have any questions about merging branches just reach out to a programmer**

	1. Make sure you are on the branch that you want to merge into
	2. Follow steps 1-5 in the General Workflow
	3. git checkout <branch name>    (Switch to the feature branch or branch that you want to merge from)
	4. Again follow steps 1-5 in the General Workflow
	5. Follow steps 1-4 in updating branches
	6. git merge <branch you're merging into>
	7. Fix any merge conflicts
	6. git status
	7. git push

Example merging test branch into dev:

	(On test branch)
	1. git status
	2. If changes exist:
	3. git add <file/folder name>
	4. git commit -m 'commit message'
	5. git push
	6. If no changes/after committing changes:
	7. git checkout dev    (Switch to dev branch)

	(On dev branch)
	8. git pull
	9. git checkout feature_TestBranch
	
	(On test branch)
	10. git merge dev
	11. If there are any conflicts, resolve them
	12. git push
	13. git checkout dev
	
	(on dev branch)
	14. git merge feature_TestBranch
	15. git push


**If you run into merge conflicts and are having trouble ask for help**

## Tagging

A tag in Git for this project is a pointer to a specific commit.
We will use tags for versioning builds. This will make it easy to refer back to different stages of the project if needed.

To create a tag on a commit, use the git tag command with one or both of the following options.

	Use the -a <tag_name> option to add a name to a tag

	Use the -m <tag_name> option to add a message to a tag

	Using no option with the tag name will create a lightweight pointer to a commit

	Follow the string of options with the commit ID of your current commit.

	To find a specific commit ID, use the git log command and copy and paste the desired ID (Only copy the hex number).

	To push the tag to the remote repository, use the git push origin tag <tag_name> command.

	To list all of the tags that have been created, use the git tag command.

## Artist Pipeline

Asset Location

PlanetaryPlanter/PlanetaryPlanter/Assets/$(The appropriate folder for the type of asset that you are adding)/

### Pushing Assets in Git (Artists)

	1. Make sure you are on the dev branch
	2. git checkout dev
	3. git status
	4. git fetch
	5. git pull (if fetch finds changes)
	6. git status
	7. git add <file/folder name>
	8. git status
	9. git commit -m 'commit message'
	10. git status
	11. git push
