#!/usr/bin/env python3
import os, asyncio, sys

class ReloadItHoe(Exception):
    pass
class GetMeOutOfHereBitch(Exception):
    pass

class shell:
    def __init__(self):
        self.version = "0.1"
        self.SHELL = "change me if you want idc"

    async def command_handler(self, command = None):
        if command is None or command == "\n" or command == " ":
            return print("You haven't supplied a command!")
        elif command == "pleaseimsodonewiththis":
            raise GetMeOutOfHereBitch()
        elif command == "reload":
            print("\nReloading! . . .\n")
            await asyncio.sleep(1)
            raise ReloadItHoe()
        else:
            try:
                exec(command)
            except Exception as e:
                print(f"\nGood job coding buddy, you got an error!\nError: {e}\n")

    async def main_shell(self):
        while True:
            try:
                command = input(self.SHELL + ": $ ")
                if command.lower() == "exit":
                    print("\nYou think it's that easy? Lmfao gj\n")
                else:
                    await self.command_handler(command=command)
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
    except ReloadItHoe:
        print("\nReloaded!\n")
        os.execv(__file__, sys.argv)
    except GetMeOutOfHereBitch:
        print("\nFine you're free. smfh\n")
