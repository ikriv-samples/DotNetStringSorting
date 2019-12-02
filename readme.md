### .NET String Sorting

It turns out that default .NET string sorting is not lexicographical, i.e.  "a"<"A", but "A"<"ab" for the invariant culture string comparer. 

More details in my blog: [.NET string comparison is not lexicographical](https://ikriv.com/blog/?p=2533)