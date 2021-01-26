#!/usr/bin/env python3
import os, asyncio, sys

class GetMeOutOfHereBitch(Exception):
    pass

class shell:
    def __init__(self):
        self.version = "0.1"

    async def command_handler(self, command=None):
        if command is None or command == "\n" or command == " ":
            return print("You haven't supplied a command!")
        elif command == "reload":
            print("\nReloading! . . .\n")
            await asyncio.sleep(1)
            raise GetMeOutOfHereBitch()

    async def main_shell(self):
        while True:
            try:
                SHELL = "root@lmao "
                command = input(SHELL + ": $ ")
                if command.lower() == "exit":
                    break
                else:
                    await self.command_handler(command=command.lower())
            except KeyboardInterrupt:
                print("""

Not so fast! Where do you think you're going! 
You're not allowed to leave me :sad:
        """)
                continue

if __name__ == "__main__":
    try:
        shell = shell()
        asyncio.get_event_loop().run_until_complete(shell.main_shell())
    except GetMeOutOfHereBitch:
        print("\nReloaded!\n")
        os.execv(__file__, sys.argv)
