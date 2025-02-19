﻿using System.Reflection;
using Xunit.Sdk;

namespace Humanizer.Tests;

/// <summary>
/// Apply this attribute to your test method to replace the
/// <see cref="Thread.CurrentThread" /> <see cref="CultureInfo.CurrentCulture" /> and
/// <see cref="CultureInfo.CurrentUICulture" /> with another culture.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class UseCultureAttribute : BeforeAfterTestAttribute
{
    readonly Lazy<CultureInfo> culture;
    CultureInfo originalCulture;
    CultureInfo originalUICulture;

    /// <summary>
    /// Replaces the culture and UI culture of the current thread with
    /// <paramref name="culture" />
    /// </summary>
    /// <param name="culture">The name of the culture.</param>
    /// <remarks>
    /// <para>
    /// This constructor overload uses <paramref name="culture" /> for both
    /// <see cref="Culture" /> and <see cref="UICulture" />.
    /// </para>
    /// </remarks>
    public UseCultureAttribute(string culture) =>
        this.culture = new(() => new(culture));

    /// <summary>
    /// Gets the culture.
    /// </summary>
    public CultureInfo Culture => culture.Value;

    /// <summary>
    /// Stores the current <see cref="CultureInfo.CurrentCulture" />
    /// <see cref="CultureInfo.CurrentCulture" /> and <see cref="CultureInfo.CurrentUICulture" />
    /// and replaces them with the new cultures defined in the constructor.
    /// </summary>
    /// <param name="methodUnderTest">The method under test</param>
    public override void Before(MethodInfo methodUnderTest)
    {
        originalCulture = CultureInfo.CurrentCulture;
        originalUICulture = CultureInfo.CurrentUICulture;

        CultureInfo.CurrentCulture = Culture;
        CultureInfo.CurrentUICulture = Culture;
    }

    /// <summary>
    /// Restores the original <see cref="CultureInfo.CurrentCulture" /> and
    /// <see cref="CultureInfo.CurrentUICulture" /> to <see cref="CultureInfo.CurrentCulture" />
    /// </summary>
    /// <param name="methodUnderTest">The method under test</param>
    public override void After(MethodInfo methodUnderTest)
    {
        CultureInfo.CurrentCulture = originalCulture;
        CultureInfo.CurrentUICulture = originalUICulture;
    }
}