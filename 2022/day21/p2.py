class Monkey:
    def __init__(self, name, job):
        self.name = name
        self.job = job
        self.val = None
        if isinstance(job, int):
            self.val = job
        else:
            self.mon1 = None
            self.mon1name = job[0].strip()
            self.mon2 = None
            self.mon2name = job[2].strip()
            self.op = job[1].strip()
            if name == "root":
                self.op = "=="

    def FinishJob(self, monkeys):
        if self.val is None:
            self.mon1 = monkeys[self.mon1name]
            self.mon2 = monkeys[self.mon2name]

    def RootJob(self):
        return self.mon1.DoJob() == self.mon2.DoJob()

    def DoJob(self):
        if self.name == "root":
            return self.RootJob()

        if self.name == "humn":
            return self.val

        if self.val is not None:
            return self.val

        job = "self.mon1.DoJob() {} self.mon2.DoJob()".format(self.op)

        return eval(job)

def CreateMonkeys(filename):
    monkeys = {}
    with open(filename) as file:
        for line in file:
            name, job = line.strip().split(":")
            jobs = job.strip().split(" ")
            if len(jobs) == 1:
                jb = int(jobs[0])
            else:
                jb = jobs
            monk = Monkey(name.strip(), jb)
            monkeys[monk.name] = monk
    
    for key, monkey in monkeys.items():
        monkey.FinishJob(monkeys)

    return monkeys

def FindHumnNumber(monkeys):
    humn = monkeys["humn"]
    root = monkeys["root"]
    num = 0
    while True:
        humn.val = num
        if root.RootJob():
            print(num)
            return num
        num += 1

monkeys = CreateMonkeys("input.in")
FindHumnNumber(monkeys)
