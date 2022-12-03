#!/usr/bin/python3
s=0
for l in open("input.txt").readlines():
    e,p=l.replace('\n','').split()
    a=ord(p)-ord('X')
    s=s+(a+{'A':1,'B':0,'C':2}[e])%3*3+a+1
print(s)