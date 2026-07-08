namespace learning_ms.Web.Infrastructure.Email.Templates;
using System.Globalization; 
public static class EmailTemplates
{
    private const string BrandNavy = "#122C4E";
    private const string BrandGold = "#D4AF37";
    private const string BrandBlue = "#2F6FED";
    private const string TextDark = "#1F2937";
    private const string TextMuted = "#5B6B82";
    private const string Success = "#16A34A";
    private const string SuccessBg = "#EAF7EF";
    private const string Danger = "#DC2626";
    private const string DangerBg = "#FDECEC";
    private const string Warning = "#D97706";
    private const string WarningBg = "#FEF6E7";
  
    public static (string Subject, string Html) Welcome(string firstName, string lastName)
    {
        var body = $"""
            <h1 style="{H1}">Welcome to LearningLadder, {Safe(firstName)}! 🎓</h1>
            <p style="{P}">Dear {Safe(firstName)} {Safe(lastName)},</p>
            <p style="{P}">
                We're thrilled to have you join <strong>LearningLadder</strong>. Whatever brought
                you here, we're committed to helping you learn, grow, and reach your goals.
            </p>
            <p style="{P}">
                Explore your dashboard, browse available courses, and don't hesitate to reach out
                if you ever need help along the way.
            </p>
            <p style="{P}">Welcome aboard,<br /><strong>The LearningLadder Team</strong></p>
        """;

        return ("Welcome to LearningLadder!", BuildLayout($"Welcome to LearningLadder, {firstName}", body));
    }
    
    public static (string Subject, string Html) OtpVerification(string firstName, string otpCode, int expiryMinutes)
    {
        var body = $"""
            <h1 style="{H1}">Verify your account</h1>
            <p style="{P}">Hi {Safe(firstName)},</p>
            <p style="{P}">
                Thanks for joining <strong>LearningLadder</strong>! Please use the one-time
                verification code below to confirm your account and get started.
            </p>
            {BuildOtpBox(otpCode)}
            <p style="{PMuted}">
                This code expires in <strong>{expiryMinutes} minutes</strong>. If you didn't
                create a LearningLadder account, you can safely ignore this email.
            </p>
        """;

        return ("Verify your LearningLadder account", BuildLayout("Your LearningLadder verification code", body));
    }
    public static (string Subject, string Html) OtpResend(string firstName, string otpCode, int expiryMinutes)
    {
        var body = $"""
            <h1 style="{H1}">Here's your new verification code</h1>
            <p style="{P}">Hi {Safe(firstName)},</p>
            <p style="{P}">
                Your previous verification code expired, so we've generated a new one for you.
                Use the code below to verify your LearningLadder account.
            </p>
            {BuildOtpBox(otpCode)}
            <p style="{PMuted}">
                This code expires in <strong>{expiryMinutes} minutes</strong>. If you didn't
                request this code, please ignore this email or contact our support team.
            </p>
        """;

        return ("Your new LearningLadder verification code", BuildLayout("A new verification code for your account", body));
    }
    public static (string Subject, string Html) MagicLink(string firstName, string magicLinkUrl, int expiryMinutes)
    {
        var body = $"""
            <h1 style="{H1}">Your sign-in link is ready</h1>
            <p style="{P}">Hi {Safe(firstName)},</p>
            <p style="{P}">
                Click the button below to securely sign in to your LearningLadder account.
                No password needed.
            </p>
            {BuildButton(magicLinkUrl, "Sign in to LearningLadder")}
            <p style="{PMuted}">
                This link expires in <strong>{expiryMinutes} minutes</strong> and can only be
                used once. If you didn't request this, you can safely ignore this email.
            </p>
            <p style="{PMutedSmall}">
                Button not working? Copy and paste this URL into your browser:<br />
                <a href="{magicLinkUrl}" style="color:{BrandBlue}; word-break:break-all;">{magicLinkUrl}</a>
            </p>
        """;

        return ("Your LearningLadder sign-in link", BuildLayout("Your secure sign-in link is ready", body));
    }
    public static (string Subject, string Html) MagicLinkResend(string firstName, string magicLinkUrl, int expiryMinutes)
    {
        var body = $"""
            <h1 style="{H1}">Here's your new sign-in link</h1>
            <p style="{P}">Hi {Safe(firstName)},</p>
            <p style="{P}">
                Your previous sign-in link expired, so we've generated a new one for you.
                Click below to sign in to your LearningLadder account.
            </p>
            {BuildButton(magicLinkUrl, "Sign in to LearningLadder")}
            <p style="{PMuted}">
                This link expires in <strong>{expiryMinutes} minutes</strong> and can only be
                used once. If you didn't request this, please ignore this email.
            </p>
            <p style="{PMutedSmall}">
                Button not working? Copy and paste this URL into your browser:<br />
                <a href="{magicLinkUrl}" style="color:{BrandBlue}; word-break:break-all;">{magicLinkUrl}</a>
            </p>
        """;

        return ("Your new LearningLadder sign-in link", BuildLayout("A new sign-in link for your account", body));
    }
    public static (string Subject, string Html) ForgotPassword(string firstName, string resetUrl, int expiryMinutes)
    {
        var body = $"""
            <h1 style="{H1}">Reset your password</h1>
            <p style="{P}">Hi {Safe(firstName)},</p>
            <p style="{P}">
                We received a request to reset the password for your LearningLadder account.
                Click the button below to choose a new password.
            </p>
            {BuildButton(resetUrl, "Reset my password")}
            {BuildAlertBox(
                $"This link expires in <strong>{expiryMinutes} minutes</strong>. If you didn't request a password reset, please ignore this email — your password will remain unchanged.",
                WarningBg, Warning, "#7A4E00")}
            <p style="{PMutedSmall}">
                Button not working? Copy and paste this URL into your browser:<br />
                <a href="{resetUrl}" style="color:{BrandBlue}; word-break:break-all;">{resetUrl}</a>
            </p>
        """;

        return ("Reset your LearningLadder password", BuildLayout("Reset your password request", body));
    }

    public static (string Subject, string Html) AccountActivation(string firstName, string activationUrl)
    {
        var body = $"""
            <h1 style="{H1}">Welcome to LearningLadder, {Safe(firstName)}! 🎓</h1>
            <p style="{P}">
                Your student profile has been created successfully. You're just one step away
                from unlocking your learning journey — please activate your account by
                clicking the button below.
            </p>
            {BuildButton(activationUrl, "Activate my account")}
            <p style="{PMuted}">
                Once activated, you'll have full access to courses, assignments, and everything
                LearningLadder has to offer.
            </p>
            <p style="{PMutedSmall}">
                Button not working? Copy and paste this URL into your browser:<br />
                <a href="{activationUrl}" style="color:{BrandBlue}; word-break:break-all;">{activationUrl}</a>
            </p>
        """;

        return ("Activate your LearningLadder account", BuildLayout("Your student profile is ready — activate it now", body));
    }
    public static (string Subject, string Html) AccountDeletion(string firstName)
    {
        var body = $"""
            <h1 style="{H1}">We're sorry to see you go</h1>
            <p style="{P}">Hi {Safe(firstName)},</p>
            <p style="{P}">
                This confirms that your LearningLadder student profile and account have been
                successfully deleted, as requested. All associated personal data will be
                handled in accordance with our data retention and privacy policy.
            </p>
            <p style="{P}">
                We're grateful for the time you spent learning with us, and we hope you found
                real value in your journey on LearningLadder. Our door is always open — you're
                welcome back anytime.
            </p>
            <p style="{P}">Wishing you continued success ahead,<br /><strong>The LearningLadder Team</strong></p>
        """;

        return ("We're sorry to see you go — LearningLadder", BuildLayout("Your LearningLadder account has been deleted", body));
    }
    public static (string Subject, string Html) CoursePurchase(string firstName, string courseName, decimal amount, string currency)
    {
        var formattedAmount = amount.ToString("N2", CultureInfo.InvariantCulture);
        var body = $"""
            <h1 style="{H1}">Thank you for your purchase! 🎉</h1>
            <p style="{P}">Hi {Safe(firstName)},</p>
            <p style="{P}">
                LearningLadder is thankful that you've chosen to invest in your growth with us.
                Your enrollment is confirmed — we hope you enjoy every step of your learning
                experience.
            </p>
            <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="margin:24px 0; border:1px solid #E7EBF2; border-radius:10px;">
              <tr>
                <td style="padding:20px 24px;">
                  <p style="margin:0 0 8px; font-size:13px; color:{TextMuted}; text-transform:uppercase; letter-spacing:0.5px;">Course</p>
                  <p style="margin:0 0 16px; font-size:17px; font-weight:700; color:{TextDark};">{Safe(courseName)}</p>
                  <p style="margin:0 0 8px; font-size:13px; color:{TextMuted}; text-transform:uppercase; letter-spacing:0.5px;">Amount paid</p>
                  <p style="margin:0; font-size:17px; font-weight:700; color:{BrandNavy};">{Safe(currency)} {formattedAmount}</p>
                </td>
              </tr>
            </table>
            <p style="{PMuted}">
                You can access your course anytime from your LearningLadder dashboard. Happy learning!
            </p>
        """;

        return ($"Your purchase is confirmed — {courseName}", BuildLayout("Thank you for your purchase on LearningLadder", body));
    }
    public static (string Subject, string Html) PromotionalDiscount(string firstName, string discountCode, string discountDescription, DateTime expiryDateUtc)
    {
        var expiryText = expiryDateUtc.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture);
        var body = $"""
            <h1 style="{H1}">A special offer, just for you ✨</h1>
            <p style="{P}">Hi {Safe(firstName)},</p>
            <p style="{P}">
                {Safe(discountDescription)}
            </p>
            <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="margin:28px 0;">
              <tr>
                <td align="center" style="background-color:{BrandNavy}; border-radius:10px; padding:24px;">
                  <p style="margin:0 0 6px; font-size:12px; color:{BrandGold}; text-transform:uppercase; letter-spacing:1px;">Your discount code</p>
                  <span style="font-size:28px; font-weight:700; letter-spacing:4px; color:#FFFFFF;">{Safe(discountCode)}</span>
                </td>
              </tr>
            </table>
            <p style="{PMuted}">
                Simply apply this code at checkout before <strong>{expiryText}</strong> to
                redeem your discount. Don't miss out!
            </p>
        """;

        return ("A special offer from LearningLadder 🎁", BuildLayout("Enjoy an exclusive discount on LearningLadder", body));
    }
    public static (string Subject, string Html) AccountBlocked(string firstName, string reason)
    {
        var body = $"""
            <h1 style="{H1}">Your account has been blocked</h1>
            <p style="{P}">Hi {Safe(firstName)},</p>
            <p style="{P}">
                After careful review, we regret to inform you that your LearningLadder account
                has been temporarily blocked due to a violation of our institution's Terms and
                Conditions.
            </p>
            {BuildAlertBox($"<strong>Reason provided:</strong> {Safe(reason)}", DangerBg, Danger, "#7A1212")}
            <p style="{P}">
                If you believe this decision was made in error, or would like to appeal, please
                reach out to our support team so we can review your case further.
            </p>
            <p style="{PMuted}">
                We take the safety and integrity of our learning community seriously, and this
                action was taken to protect the interests of all LearningLadder students and staff.
            </p>
        """;

        return ("Important: Your LearningLadder account has been blocked", BuildLayout("An update regarding your LearningLadder account", body));
    }
    public static (string Subject, string Html) AccountUnblocked(string firstName)
    {
        var body = $"""
            <h1 style="{H1}">Your account has been reinstated</h1>
            <p style="{P}">Hi {Safe(firstName)},</p>
            <p style="{P}">
                Following a thorough review of your account and conduct, we're pleased to let you
                know that your LearningLadder account has been unblocked and full access has been
                restored.
            </p>
            {BuildAlertBox(
                "Our review found that your conduct now aligns with LearningLadder's institutional policies and community guidelines.",
                SuccessBg, Success, "#0F5132")}
            <p style="{P}">
                We appreciate your patience throughout this process and look forward to continuing
                to support your learning journey.
            </p>
        """;

        return ("Good news: Your LearningLadder account is active again", BuildLayout("Your LearningLadder account has been unblocked", body));
    }
    public static (string Subject, string Html) StaffWelcome(string firstName, string lastName, string role)
    {
        var body = $"""
            <h1 style="{H1}">Welcome to the team, {Safe(firstName)}! 🎓</h1>
            <p style="{P}">Dear {Safe(firstName)} {Safe(lastName)},</p>
            <p style="{P}">
                We are delighted to welcome you to <strong>LearningLadder</strong> as our new
                <strong>{Safe(role)}</strong>. Your expertise and passion for education make you
                a wonderful addition to our institution.
            </p>
            <p style="{P}">
                Our team is excited to work alongside you as we continue building a place where
                students can learn, grow, and thrive. You'll be receiving onboarding details and
                access to your staff dashboard shortly.
            </p>
            <p style="{P}">
                Once again, welcome aboard — here's to a great journey together!
            </p>
            <p style="{P}">Warm regards,<br /><strong>The LearningLadder Team</strong></p>
        """;

        return ("Welcome to LearningLadder!", BuildLayout($"Welcome to LearningLadder, {firstName}", body));
    }
  
    public static (string Subject, string Html) AdmissionGranted(string firstName, string programName, string academicYear, string portalUrl)
        {
            var body = $"""
                <h1 style="{H1}">Congratulations, you've been admitted! 🎉</h1>
                <p style="{P}">Dear {Safe(firstName)},</p>
                <p style="{P}">
                    We are delighted to inform you that your application to <strong>LearningLadder</strong>
                    has been successful. You have been offered admission for the following program:
                </p>
                <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="margin:24px 0; border:1px solid #E7EBF2; border-radius:10px;">
                  <tr>
                    <td style="padding:20px 24px;">
                      <p style="margin:0 0 8px; font-size:13px; color:{TextMuted}; text-transform:uppercase; letter-spacing:0.5px;">Program</p>
                      <p style="margin:0 0 16px; font-size:17px; font-weight:700; color:{TextDark};">{Safe(programName)}</p>
                      <p style="margin:0 0 8px; font-size:13px; color:{TextMuted}; text-transform:uppercase; letter-spacing:0.5px;">Academic year</p>
                      <p style="margin:0; font-size:17px; font-weight:700; color:{BrandNavy};">{Safe(academicYear)}</p>
                    </td>
                  </tr>
                </table>
                <p style="{P}">
                    Please log in to your student portal to accept your offer, complete registration,
                    and begin preparing for your journey with us.
                </p>
                {BuildButton(portalUrl, "Go to my student portal")}
                <p style="{PMuted}">Welcome to the LearningLadder family — we can't wait to see you thrive.</p>
            """;
     
            return ("Congratulations! Your LearningLadder admission is confirmed", BuildLayout("You've been admitted to LearningLadder", body));
        }
    
        public static (string Subject, string Html) AdmissionRejected(string firstName, string programName, string reason)
        {
            var body = $"""
                <h1 style="{H1}">An update on your application</h1>
                <p style="{P}">Dear {Safe(firstName)},</p>
                <p style="{P}">
                    Thank you for applying to <strong>{Safe(programName)}</strong> at LearningLadder.
                    After careful review by our admissions committee, we regret to inform you that we
                    are unable to offer you admission at this time.
                </p>
                {BuildAlertBox($"<strong>Reviewer's note:</strong> {Safe(reason)}", WarningBg, Warning, "#7A4E00")}
                <p style="{P}">
                    This decision does not reflect your full potential, and we encourage you to apply
                    again in a future intake. We wish you every success in your academic journey.
                </p>
                <p style="{P}">Warm regards,<br /><strong>LearningLadder Admissions Committee</strong></p>
            """;
     
            return ("An update on your LearningLadder application", BuildLayout("An update regarding your admission application", body));
        }
        public static (string Subject, string Html) TimetableCreated(string firstName, string termName, string timetableUrl)
        {
            var body = $"""
                <h1 style="{H1}">Your timetable is ready 🗓️</h1>
                <p style="{P}">Hi {Safe(firstName)},</p>
                <p style="{P}">
                    Your class timetable for <strong>{Safe(termName)}</strong> has been created
                    successfully. You can view your full schedule, including class times and venues,
                    from your student dashboard.
                </p>
                {BuildButton(timetableUrl, "View my timetable")}
                <p style="{PMuted}">Please review your schedule carefully to avoid any clashes or missed classes.</p>
            """;
     
            return ($"Your {termName} timetable is ready", BuildLayout("Your class timetable has been created", body));
        }
        
        public static (string Subject, string Html) BookBorrowed(string firstName, string bookTitle, DateTime dueDateUtc)
        {
            var dueText = dueDateUtc.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture);
            var body = $"""
                <h1 style="{H1}">Library checkout confirmed 📚</h1>
                <p style="{P}">Hi {Safe(firstName)},</p>
                <p style="{P}">
                    This confirms that you've borrowed the following book from the LearningLadder Library:
                </p>
                <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="margin:24px 0; border:1px solid #E7EBF2; border-radius:10px;">
                  <tr>
                    <td style="padding:20px 24px;">
                      <p style="margin:0 0 8px; font-size:13px; color:{TextMuted}; text-transform:uppercase; letter-spacing:0.5px;">Book title</p>
                      <p style="margin:0 0 16px; font-size:17px; font-weight:700; color:{TextDark};">{Safe(bookTitle)}</p>
                      <p style="margin:0 0 8px; font-size:13px; color:{TextMuted}; text-transform:uppercase; letter-spacing:0.5px;">Due date</p>
                      <p style="margin:0; font-size:17px; font-weight:700; color:{BrandNavy};">{dueText}</p>
                    </td>
                  </tr>
                </table>
                <p style="{PMuted}">
                    Please return the book on or before the due date to avoid late fees. Happy reading!
                </p>
            """;
     
            return ($"Library checkout: {bookTitle}", BuildLayout("Your library book checkout is confirmed", body));
        }
        public static (string Subject, string Html) ExamPreparation(string firstName, string examName, DateTime examDateUtc, string instructions)
        {
            var examDateText = examDateUtc.ToString("dddd, MMMM d, yyyy 'at' h:mm tt", CultureInfo.InvariantCulture);
            var body = $"""
                <h1 style="{H1}">Time to prepare for your exam 📝</h1>
                <p style="{P}">Hi {Safe(firstName)},</p>
                <p style="{P}">
                    This is a reminder that your upcoming exam is scheduled as follows:
                </p>
                <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="margin:24px 0; border:1px solid #E7EBF2; border-radius:10px;">
                  <tr>
                    <td style="padding:20px 24px;">
                      <p style="margin:0 0 8px; font-size:13px; color:{TextMuted}; text-transform:uppercase; letter-spacing:0.5px;">Exam</p>
                      <p style="margin:0 0 16px; font-size:17px; font-weight:700; color:{TextDark};">{Safe(examName)}</p>
                      <p style="margin:0 0 8px; font-size:13px; color:{TextMuted}; text-transform:uppercase; letter-spacing:0.5px;">Date &amp; time</p>
                      <p style="margin:0; font-size:17px; font-weight:700; color:{BrandNavy};">{examDateText}</p>
                    </td>
                  </tr>
                </table>
                {BuildAlertBox(Safe(instructions), WarningBg, Warning, "#7A4E00")}
                <p style="{PMuted}">We wish you focused preparation and the very best of luck!</p>
            """;
     
            return ($"Reminder: {examName} is coming up", BuildLayout("Prepare for your upcoming exam", body));
        }
        public static (string Subject, string Html) ExamPassed(string firstName, string examName, string grade)
        {
            var body = $"""
                <h1 style="{H1}">Congratulations, you passed! 🏆</h1>
                <p style="{P}">Hi {Safe(firstName)},</p>
                <p style="{P}">
                    Great news! You have successfully passed <strong>{Safe(examName)}</strong>.
                    LearningLadder is proud of your hard work and dedication.
                </p>
                <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="margin:28px 0;">
                  <tr>
                    <td align="center" style="background-color:{SuccessBg}; border-radius:10px; padding:24px;">
                      <p style="margin:0 0 6px; font-size:12px; color:{Success}; text-transform:uppercase; letter-spacing:1px;">Your result</p>
                      <span style="font-size:28px; font-weight:700; color:#0F5132;">{Safe(grade)}</span>
                    </td>
                  </tr>
                </table>
                <p style="{PMuted}">Keep up the excellent work — your progress reflects real dedication!</p>
            """;
     
            return ($"Congratulations on passing {examName}!", BuildLayout($"You passed {examName} — congratulations!", body));
        }
        public static (string Subject, string Html) QuizPassed(string firstName, string quizName, string score)
        {
            var body = $"""
                <h1 style="{H1}">Well done on your quiz! 🌟</h1>
                <p style="{P}">Hi {Safe(firstName)},</p>
                <p style="{P}">
                    You've successfully passed <strong>{Safe(quizName)}</strong>. Nice work — every quiz
                    you complete brings you one step closer to mastering your course material.
                </p>
                <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="margin:28px 0;">
                  <tr>
                    <td align="center" style="background-color:{SuccessBg}; border-radius:10px; padding:24px;">
                      <p style="margin:0 0 6px; font-size:12px; color:{Success}; text-transform:uppercase; letter-spacing:1px;">Your score</p>
                      <span style="font-size:28px; font-weight:700; color:#0F5132;">{Safe(score)}</span>
                    </td>
                  </tr>
                </table>
                <p style="{PMuted}">Keep the momentum going — your next lesson is waiting on your dashboard!</p>
            """;
     
            return ($"You passed {quizName}!", BuildLayout($"You passed {quizName} — great job!", body));
        }
  
        public static (string Subject, string Html) TutorAccountActivation(string firstName, string activationUrl)
        {
            var body = $"""
                <h1 style="{H1}">Welcome, {Safe(firstName)} — let's activate your profile</h1>
                <p style="{P}">
                    Your tutor profile has been created on <strong>LearningLadder</strong>. Please check
                    your inbox and click the button below to verify your account before you can start
                    teaching and managing your courses.
                </p>
                {BuildButton(activationUrl, "Verify my tutor account")}
                <p style="{PMuted}">
                    Once verified, you'll gain full access to your tutor dashboard, class materials,
                    and student rosters.
                </p>
                <p style="{PMutedSmall}">
                    Button not working? Copy and paste this URL into your browser:<br />
                    <a href="{activationUrl}" style="color:{BrandBlue}; word-break:break-all;">{activationUrl}</a>
                </p>
            """;
     
            return ("Verify your LearningLadder tutor account", BuildLayout("Activate your new tutor profile", body));
        }
  
        public static (string Subject, string Html) TutorAccountBlocked(string firstName, string reason)
        {
            var body = $"""
                <h1 style="{H1}">Your tutor account has been suspended</h1>
                <p style="{P}">Dear {Safe(firstName)},</p>
                <p style="{P}">
                    Following a review by our administration team, your tutor account and profile on
                    <strong>LearningLadder</strong> have been suspended due to a violation of our
                    institution's policies.
                </p>
                {BuildAlertBox($"<strong>Reason provided:</strong> {Safe(reason)}", DangerBg, Danger, "#7A1212")}
                <p style="{P}">
                    If you believe this decision was made in error or would like to appeal, please
                    contact our administration team so we can review your case further.
                </p>
                <p style="{PMuted}">
                    This action was taken to uphold the standards and safety of the LearningLadder
                    learning community.
                </p>
            """;
     
            return ("Important: Your LearningLadder tutor account has been suspended", BuildLayout("An update regarding your tutor account", body));
        }
  
        public static (string Subject, string Html) StudentAnnouncement(string title, string message, string? ctaLabel, string? ctaUrl)
        {
            var cta = !string.IsNullOrWhiteSpace(ctaUrl) && !string.IsNullOrWhiteSpace(ctaLabel)
                ? BuildButton(ctaUrl!, ctaLabel!)
                : string.Empty;
     
            var body = $"""
                <h1 style="{H1}">{Safe(title)}</h1>
                <p style="{P}">Dear Student,</p>
                <p style="{P}">{Safe(message)}</p>
                {cta}
                <p style="{PMuted}">Thank you for being part of the LearningLadder community.</p>
            """;
     
            return (title, BuildLayout($"An announcement from LearningLadder: {title}", body));
        }
        public static (string Subject, string Html) TutorAnnouncement(string title, string message, string? ctaLabel, string? ctaUrl)
        {
            var cta = !string.IsNullOrWhiteSpace(ctaUrl) && !string.IsNullOrWhiteSpace(ctaLabel)
                ? BuildButton(ctaUrl!, ctaLabel!)
                : string.Empty;
     
            var body = $"""
                <h1 style="{H1}">{Safe(title)}</h1>
                <p style="{P}">Dear Tutor,</p>
                <p style="{P}">{Safe(message)}</p>
                {cta}
                <p style="{PMuted}">Thank you for your continued dedication to our students at LearningLadder.</p>
            """;
     
            return (title, BuildLayout($"An announcement from LearningLadder: {title}", body));
        }
        
        public static (string Subject, string Html) AccommodationProvided(string firstName, string hostelName, string roomNumber, DateTime checkInDateUtc)
        {
            var checkInText = checkInDateUtc.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture);
            var body = $"""
                <h1 style="{H1}">Your accommodation is confirmed 🏠</h1>
                <p style="{P}">Hi {Safe(firstName)},</p>
                <p style="{P}">
                    We're pleased to let you know that LearningLadder has arranged accommodation for you
                    for the upcoming academic session. Details are below:
                </p>
                <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="margin:24px 0; border:1px solid #E7EBF2; border-radius:10px;">
                  <tr>
                    <td style="padding:20px 24px;">
                      <p style="margin:0 0 8px; font-size:13px; color:{TextMuted}; text-transform:uppercase; letter-spacing:0.5px;">Hostel / residence</p>
                      <p style="margin:0 0 16px; font-size:17px; font-weight:700; color:{TextDark};">{Safe(hostelName)}</p>
                      <p style="margin:0 0 8px; font-size:13px; color:{TextMuted}; text-transform:uppercase; letter-spacing:0.5px;">Room number</p>
                      <p style="margin:0 0 16px; font-size:17px; font-weight:700; color:{TextDark};">{Safe(roomNumber)}</p>
                      <p style="margin:0 0 8px; font-size:13px; color:{TextMuted}; text-transform:uppercase; letter-spacing:0.5px;">Check-in date</p>
                      <p style="margin:0; font-size:17px; font-weight:700; color:{BrandNavy};">{checkInText}</p>
                    </td>
                  </tr>
                </table>
                <p style="{PMuted}">Please arrive with a valid ID and your admission letter for check-in verification.</p>
            """;
     
            return ("Your LearningLadder accommodation is confirmed", BuildLayout("Your accommodation arrangement details", body));
        }
    
    private const string H1 = $"margin:0 0 20px; font-size:22px; font-weight:700; color:{TextDark}; line-height:1.3;";
    private const string P = $"margin:0 0 16px; font-size:15px; color:{TextDark}; line-height:1.6;";
    private const string PMuted = $"margin:16px 0 0; font-size:13px; color:{TextMuted}; line-height:1.6;";
    private const string PMutedSmall = $"margin:24px 0 0; font-size:12px; color:{TextMuted}; line-height:1.5;";
    private static string BuildLayout(string previewText, string bodyHtml)
    {
        return $"""
        <!DOCTYPE html>
        <html lang="en">
        <head>
        <meta charset="UTF-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <meta name="color-scheme" content="light" />
        <title>LearningLadder</title>
        </head>
        <body style="margin:0; padding:0; background-color:#F1F4F9; font-family:'Segoe UI', Helvetica, Arial, sans-serif;">
          <span style="display:none; font-size:1px; color:#F1F4F9; line-height:1px; max-height:0; max-width:0; opacity:0; overflow:hidden;">{previewText}</span>
          <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="background-color:#F1F4F9; padding:32px 16px;">
            <tr>
              <td align="center">
                <table role="presentation" width="600" cellpadding="0" cellspacing="0" style="width:600px; max-width:600px; background-color:#FFFFFF; border-radius:12px; overflow:hidden; box-shadow:0 2px 10px rgba(15,32,67,0.08);">
                  <tr>
                    <td style="background-color:{BrandNavy}; padding:28px 40px;" align="center">
                      <span style="font-size:22px; font-weight:700; color:#FFFFFF; letter-spacing:0.5px;">
                        🎓 Learning<span style="color:{BrandGold};">Ladder</span>
                      </span>
                    </td>
                  </tr>
                  <tr><td style="height:4px; background-color:{BrandGold}; line-height:4px; font-size:0;">&nbsp;</td></tr>
                  <tr>
                    <td style="padding:40px;">
                      {bodyHtml}
                    </td>
                  </tr>
                  <tr>
                    <td style="background-color:#F7F9FC; padding:24px 40px; border-top:1px solid #E7EBF2;" align="center">
                      <p style="margin:0 0 6px; font-size:13px; color:{TextMuted};">&copy; {DateTime.UtcNow.Year} LearningLadder. All rights reserved.</p>
                      <p style="margin:0; font-size:12px; color:#9AA7B8;">This is an automated message — please do not reply directly to this email.</p>
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </body>
        </html>
        """;
    }
    private static string BuildButton(string url, string label, string color = BrandBlue)
    {
        return $"""
        <table role="presentation" cellpadding="0" cellspacing="0" style="margin:28px 0;">
          <tr>
            <td style="border-radius:8px; background-color:{color};">
              <a href="{url}" target="_blank" style="display:inline-block; padding:14px 32px; font-size:15px; font-weight:600; color:#FFFFFF; text-decoration:none; border-radius:8px;">{label}</a>
            </td>
          </tr>
        </table>
        """;
    }
    private static string BuildOtpBox(string otpCode)
    {
        var spaced = string.Join(" ", otpCode.ToCharArray());
        return $"""
        <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="margin:28px 0;">
          <tr>
            <td align="center" style="background-color:#F1F4F9; border:1px dashed #C7D2E2; border-radius:10px; padding:20px;">
              <span style="font-size:32px; font-weight:700; letter-spacing:8px; color:{BrandNavy};">{spaced}</span>
            </td>
          </tr>
        </table>
        """;
    }
    private static string BuildAlertBox(string message, string bgColor, string borderColor, string textColor)
    {
        return $"""
        <table role="presentation" width="100%" cellpadding="0" cellspacing="0" style="margin:20px 0;">
          <tr>
            <td style="background-color:{bgColor}; border-left:4px solid {borderColor}; border-radius:6px; padding:16px 20px;">
              <p style="margin:0; font-size:14px; color:{textColor}; line-height:1.5;">{message}</p>
            </td>
          </tr>
        </table>
        """;
    }
    private static string Safe(string value) =>
        string.IsNullOrEmpty(value)
            ? string.Empty
            : value.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
}
