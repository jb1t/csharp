## Azure Function

I wanted to create a simple Azure Function to make an HTTP GET Request to my website, which is hosted on the cheapest GoDaddy subscription. This subscription is on shared hosting and I don't have much control over it. The initial load time of the WordPress site is CRAZY slow... in the ~22 second range! So I thought I'd create a keepalive function. At first I created a SUPER simple Python script to do this, and it worked well..... Until it didn't. I wasn't home for a couple of days and it stopped working. I was running it on a RPi at home. However, I didn't have any kind of VPN setup, so I could login remotely to see what the issue was. I thought, heck, I'll create a super quick Azure Function based on a timer to ping the sight instead. That way I can see if there are issues online anywhere I have internet access and potential fix the issue also.

I did that and it worked well. However, I started to notice some issues... The interval not kicking off every 4 mins like I set it up to do, and some communication service errors. The communication service error I attribute to potentially the sight being down at GoDaddy for maintenance or something similar. The not kicking off??? not sure.

I decided to replace all of this with a Azure Logic App. I'll reference that in the near future.
