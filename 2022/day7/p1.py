class File:
    def __init__(self, name, size, directory):
        self.name = name
        self.size = size
        self.directory = directory

class Directory:
    def __init__(self, name):
        self.name = name
        self.files = {}
        self.directories = {}

    def AddFile(self, name, size):
        if name not in self.files:
            self.files[name] = File(name, size, self)
    
    def AddDirectory(self, name):
        if name not in self.directories:
            self.directories[name] = Directory(self, name)
    
    def Size(self):
        size = 0
        for key, file in self.files.items():
            size += file.size
        for key, dir in self.directories.items():
            size += dir.size
        
        return size
    
    def PrintTree(self, depth = 0):
        dp = ' ' * depth
        print(depth, '-', self.name, '(dir)')
        for key, dir in self.directories.items():
            dir.PrintTree(depth + 1)
        for key, file in self.files.items():
            print(depth, '-', file.name, '(file, size=', 585, ')')




class Filesystem:
    def __init__(self):
        self.directories = []