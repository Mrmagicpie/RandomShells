#!/bin/bash

version="0.1"
LOCATION=${PWD/#$HOME/\~}
NOTHING=""

if ! [ -f "$HOME/.mrmshell" ]; then
	echo
	echo "Not working customly! Using built-in options!"
	echo
	SHELL="$USER@MrmagicpieShell - $version"
else
	. $HOME/.mrmshell
fi

echo
echo "Entering Mrmagicpie Shell"
echo "Version: $version!"
echo

command_handler()
{
if [ $1 = "exit" ]; then
	echo
	echo "Exiting Mrmagicie Shell!"
	echo
	# sleep 1
	exit
elif [ $1 = "cd" ]; then
	cd="${1//cd /$nothing}"
	echo "The cd variable is: $cd"
	if ! [ -d "$cd" ]; then
		echo
		echo "Not a valid location!"
		echo
	else
		LOCATION="$LOCATION/$1"
		SHELL="root@lmao [ $LOCATION ]"
		cd $cd
	fi
else
	$1
fi
}

while true; do
	read -r -p "$SHELL: $ " output
	if ! [ -z "$output" ]; then
		# echo "e: $output"
		case $output in
			print) echo up ;;
			B) echo down ;;
			C) echo right ;;
			D) echo left ;;
			*) command_handler $output
		esac
	fi
done
