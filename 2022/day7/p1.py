class File:
    def __init__(self, name, size, directory):
        self.name = name.strip()
        self.size = size
        self.directory = directory

class Directory:
    def __init__(self, name, parent):
        self.name = name.strip()
        self.files = {}
        self.directories = {}
        self.parent = parent

    def AddFile(self, name, size):
        if name not in self.files:
            self.files[name] = File(name, size, self)
        
        return self.files[name]
    
    def AddDirectory(self, name):
        if name not in self.directories:
            self.directories[name] = Directory(name, self)

        return self.directories[name]
    
    def Size(self):
        size = 0
        for key, file in self.files.items():
            size += file.size
        for key, dir in self.directories.items():
            size += dir.size
        
        return size
    
    def PrintTree(self, depth = 0):
        dp = ' ' * depth
        dpf = ' ' * (depth + 1)
        print('{} - {} (dir)'.format(dp, self.name))
        for key, dir in self.directories.items():
            dir.PrintTree(depth + 1)
        for key, file in self.files.items():
            print('{} - {} (file, size={})'.format(dpf, file.name, file.size))


class Filesystem:
    def __init__(self):
        self.root = None
        self.currentdir = None
        self.lsdir = None

    def PrintSystemTree(self):
        self.root.PrintTree()
    
    def ProcessInput(self, filename):
        with open(filename) as file:
            for line in file:
                if(line.startswith("$")):
                    self.ExecuteCommand(line)
                else:
                    self.ReadOutput(line)

    def ExecuteCommand(self, command):
        comm = command.split(" ")
        if(comm[1] == 'cd'):
            self.ChangeDirectory(comm[2].strip())
        elif(comm[1] == 'ls'):
            pass # Nothing to do (yet)

    def ChangeDirectory(self, name):
        if(self.root is None):
            self.root = Directory('/', None)
            self.currentdir = self.root
        elif(name == ".."):
            print("what")
            if(self.currentdir.parent is not None):
                self.currentdir = self.currentdir.parent
        else:
            dir = self.currentdir.AddDirectory(name) # Creates directory if required. Returns the new or already existing directory.
            self.currentdir = dir

    def ReadOutput(self, output):
        out = output.split(" ")
        if(out[0] == "dir"):
            dir = self.currentdir.AddDirectory(out[1].strip()) # Creates directory if required. Returns the new or already existing directory.
        else:
            file = self.currentdir.AddFile(out[1].strip(), out[0].strip()) # Creates file if required. Returns the new or already existing file.
    
    
fs = Filesystem()
fs.ProcessInput("test.in")
fs.PrintSystemTree()