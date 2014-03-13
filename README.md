MagicExpression
===============

Regular expression made simple in .Net


MagicExpression for muggles
---------------------------

    var magicWand = Magex.New();

    magicWand.Character('-').Repeat.AtMostOnce()
             .CharacterIn(Characters.Numeral).Repeat.Any()
             .Character('.')
             .CharacterIn(Characters.Numeral).Repeat.AtLeastOnce();

    // Creates a regex corresponding to
    // -?[0-9]*\.[0-9]+
    var floatingPointNumberDetector = new Regex(magicWand.Expression);

    Assert.IsTrue(floatingPointNumberDetector.IsMatch("1.234"));
    Assert.IsTrue(floatingPointNumberDetector.IsMatch("-1.234"));
    Assert.IsTrue(floatingPointNumberDetector.IsMatch("0.0"));

    Assert.IsFalse(floatingPointNumberDetector.IsMatch("0"));
    Assert.IsFalse(floatingPointNumberDetector.IsMatch("1,234"));
    Assert.IsFalse(floatingPointNumberDetector.IsMatch("0x234"));
    Assert.IsFalse(floatingPointNumberDetector.IsMatch("#1a4f66"));

### Add content to the MagicExpression

#### Characters

All of these functions are escaping special chars.

* `Character()` matches any character
* `Character(char)` matches the given character
* `CharacterIn(params char[])` matches any character in the list
* `CharacterIn(string)` matches any character in the list
* `CharacterIn(Characters, string)` matches any character in the list
* `CharacterIn(Characters params char[])` matches any character in the list
* `CharacterNotIn(params char[])` matches any character not in the list
* `CharacterNotIn(string)` matches any character not in the list
* `CharacterNotIn(Characters, string)` matches any character not in the list
* `CharacterNotIn(Characters params char[])` matches any character not in the list

`Characters` is an enumeration of flags, allowing you to add special characters to the list of matched characters.

#### Strings

* `String(string)` matches the given string, escaping special chars
     
### Repeat last element

Some content can be repeated. For this content, you can use the Repeat property and specify cardinality.

* `Repeat.Any()` will match any number of repetitions, including 0
* `Repeat.Once()` default behavior
* `Repeat.Times(uint)`
* `Repeat.Between(uint, uint)`
* `Repeat.AtLeastOnce()`
* `Repeat.AtMostOnce()`
    
### Groups, captures and backreferences

Usage
     
    var magicWand = Magex.New();
    var notSoMagicWand = Magex.New();

    // You can use sub expressions, created before
    // or create them in place
    magicWand.Group(notSoMagicWand.Character('a'));
    magicWand.Group(Magex.New().Character('a'));

    // Or use lambdas
    magicWand.Group(x => x.Character('a'));

    magicWand = Magex.New();
    // Reference a previous capture
    // Will match something like <strong>hello world</strong> (but please don't parse HTML with Magex in real life)
    magicWand.Character('<')
             .CaptureAs("tag", x => x.CharacterNotIn('>').Repeat.AtLeastOnce())
             .Character('>')
             .Character().Repeat.Any().Lazy()
             .String("</")
             .BackReference("tag")
             .Character('>');

    var badHtmlTagDetector = new Regex(magicWand.Expression);

    Assert.IsTrue(badHtmlTagDetector.IsMatch("<strong>hello world</strong>"));
    Assert.IsTrue(badHtmlTagDetector.IsMatch("<h1>A title</h1>"));

    Assert.IsFalse(badHtmlTagDetector.IsMatch("<h1>A tag mismatch</strong>"));

* `Group(IExpressionElement)` Non capturing group
* `Group(Action<IMagex>)` Non capturing group defined with a lambda
* `Capture(IExpressionElement)` Indexed capture
* `Capture(Action<IMagex>)` Indexed capture defined with a lambda
* `CaptureAs(string, IExpressionElement)` Named capture
* `CaptureAs(string, Action<IMagex>)` Named capture defined with a lambda
* `BackReference(string)` references a named capture
* `BackReference(uint)` references an indexed capture

### Alternatives

Usage

    var magicWand = Magex.New();

    // With subexpressions
    magicWand.Alternative(
        Magex.New().Character('a'),
        Magex.New().Character('b')
        );

    var notSoMagicWand = Magex.New();
    // With lambdas
    notSoMagicWand.Alternative(
        x => x.Character('a'),
        x => x.Character('b')
        );

    var alternativeDetector = new Regex(notSoMagicWand.Expression);

    Assert.IsTrue(alternativeDetector.IsMatch("a"));
    Assert.IsTrue(alternativeDetector.IsMatch("b"));

    Assert.IsFalse(alternativeDetector.IsMatch("c"));

* `Alternative(params IExpressionElement[])` Matches the string if it corresponds to one alternative
* `Alternative(params Action<IMagex>[])` Same thing with 

### Laziness

    // The group will match the smallest ensemble possible, e.g. "<em>" and "</em>"
    var lazyMagicWand = Magex.New().Character('<')
             .Group(x => x.Character().Repeat.Any().Lazy())
             .Character('>');

    var lazyDetector = new Regex(lazyMagicWand.Expression);
    var matchCollection = lazyDetector.Matches("<em>something</em>");
    Assert.AreEqual(2, matchCollection.Count);

    // The group will match the larges ensemble possible, e.g. the whole "<em>something</em>"
    var greedyMagicWand = Magex.New().Character('<')
             .Group(x => x.Character().Repeat.Any())
             .Character('>');

    var greedyDetector = new Regex(greedyMagicWand.Expression);
    matchCollection = greedyDetector.Matches("<em>something</em>");
    Assert.AreEqual(1, matchCollection.Count);
          
* `Lazy()` Makes the detection lazy instead of greedy (default)          
          
### Predefined Expressions

Usage

    var magicWand = Magex.New();

    magicWand.Literal(Magex.NumericRange(0, 42));

    var detector = new Regex(magicWand.Expression);

    Assert.IsTrue(detector.IsMatch("0"));
    Assert.IsTrue(detector.IsMatch("9"));
    Assert.IsTrue(detector.IsMatch("20"));
    Assert.IsTrue(detector.IsMatch("42"));
    Assert.IsFalse(detector.IsMatch("43"));
    Assert.IsFalse(detector.IsMatch("52"));

* 'Literal(string)' inserts a regular expression

## License
Licensed under MIT License, Copyright 2014 Guillaume Gautreau, Timothée Bourguignon
