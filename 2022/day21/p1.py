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

    def FinishJob(self, monkeys):
        if self.val is None:
            self.mon1 = monkeys[self.mon1name]
            self.mon2 = monkeys[self.mon2name]

    def DoJob(self):
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

monkeys = CreateMonkeys("input.in")
root = monkeys["root"]
print(root.DoJob())