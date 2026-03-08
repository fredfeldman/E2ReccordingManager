# Profile Management Guide

## Overview
The Enigma2 Recording Manager now supports multiple device profiles, allowing you to easily manage and switch between different Enigma2 receivers.

## Connection Dialog Layout

```
┌──────────────────────────────────────────────────────────┐
│  Connect to Enigma2 Device                          [X]  │
├──────────────────────────────────────────────────────────┤
│  ╔════════════════════════════════════════════════════╗  │
│  ║  Profile Management                                ║  │
│  ╠════════════════════════════════════════════════════╣  │
│  ║  Load Profile:    [Living Room VU+      ▼]        ║  │
│  ║                                   [Save Profile]   ║  │
│  ║                                                    ║  │
│  ║  Profile Name:    [Living Room VU+          ]     ║  │
│  ║                                   [Delete Profile] ║  │
│  ╚════════════════════════════════════════════════════╝  │
│                                                          │
│  ╔════════════════════════════════════════════════════╗  │
│  ║  Connection Settings                               ║  │
│  ╠════════════════════════════════════════════════════╣  │
│  ║  Host:         [192.168.1.100              ]       ║  │
│  ║                                                    ║  │
│  ║  Port:         [80        ]                        ║  │
│  ║                                                    ║  │
│  ║  Username:     [root                       ]       ║  │
│  ║                                                    ║  │
│  ║  Password:     [**********                 ]       ║  │
│  ╚════════════════════════════════════════════════════╝  │
│                                                          │
│                               [Connect]  [Cancel]        │
└──────────────────────────────────────────────────────────┘
```

## Workflow Examples

### Scenario 1: First Time User

**Step 1: Initial Setup**
- Launch application
- Click "Connect" button
- See empty/default profile dropdown

**Step 2: Create First Profile**
```
Profile Name: Living Room VU+
Host: 192.168.1.100
Port: 80
Username: root
Password: mypassword
```
- Click "Save Profile"
- Click "Connect"

**Result:** 
- Profile saved to `%AppData%\E2RecordingManager\connections.json`
- Marked as last used profile
- Connected to device

### Scenario 2: Adding Second Device

**Step 1: Open Connection Dialog**
- Already connected to first device
- Click "Disconnect"
- Click "Connect"

**Step 2: Create New Profile**
- Enter new profile name: "Bedroom Dreambox"
- Enter different device IP: 192.168.1.101
- Enter credentials
- Click "Save Profile"
- Click "Connect"

**Result:**
- Two profiles now saved
- Connected to second device
- Second device marked as last used

### Scenario 3: Switching Between Devices

**Step 1: Disconnect Current Device**
- Click "Disconnect"

**Step 2: Select Different Profile**
- Click "Connect"
- In "Load Profile" dropdown, see:
  ```
  ▼ Bedroom Dreambox    ← Currently selected (last used)
    Living Room VU+
  ```
- Select "Living Room VU+"
- Settings auto-fill
- Click "Connect"

**Result:**
- Connected to first device
- First device now marked as last used

### Scenario 4: Updating a Profile

**Situation:** IP address changed for "Living Room VU+"

**Step 1: Load Profile**
- Click "Connect"
- Select "Living Room VU+" from dropdown
- Settings load automatically

**Step 2: Update Settings**
- Change Host from 192.168.1.100 to 192.168.1.105
- Click "Save Profile"
- Confirm overwrite when prompted

**Step 3: Connect**
- Click "Connect"

**Result:**
- Profile updated with new IP
- Successfully connected to device at new address

### Scenario 5: Removing Old Device

**Situation:** Sold "Bedroom Dreambox"

**Step 1: Select Profile to Delete**
- Click "Connect"
- Select "Bedroom Dreambox" from dropdown

**Step 2: Delete**
- Click "Delete Profile"
- Confirm deletion

**Result:**
- Profile removed from list
- File updated in AppData
- Dropdown now only shows remaining profiles

## Profile Storage Details

### File Location
```
Windows: C:\Users\[YourName]\AppData\Roaming\E2RecordingManager\
```

### Files Created
```
E2RecordingManager\
├── connections.json       ← All saved profiles
└── lastprofile.txt       ← Name of last connected profile
```

### connections.json Structure
```json
[
  {
    "Name": "Living Room VU+",
    "Host": "192.168.1.100",
    "Port": 80,
    "Username": "root",
    "Password": "password123"
  },
  {
    "Name": "Bedroom Dreambox",
    "Host": "192.168.1.101",
    "Port": 80,
    "Username": "root",
    "Password": "password456"
  },
  {
    "Name": "Office DVB-S2",
    "Host": "192.168.1.102",
    "Port": 8080,
    "Username": "admin",
    "Password": "admin"
  }
]
```

### lastprofile.txt Content
```
Living Room VU+
```

## Benefits of Profile Management

### ✅ Convenience
- No need to re-enter connection details
- Quick switching between devices
- Automatic loading of last used profile

### ✅ Organization
- Meaningful names for each device
- Easy to identify which device is which
- Separate credentials per device

### ✅ Efficiency
- Save time switching between devices
- Bulk manage recordings from multiple receivers
- Work with home and remote devices

### ✅ Safety
- Credentials stored locally
- No need to remember multiple passwords
- Easy to update when credentials change

## Common Use Cases

### Multiple Rooms
```
Home Setup:
├── Living Room VU+ (192.168.1.100)
├── Bedroom Dreambox (192.168.1.101)
└── Kids Room Receiver (192.168.1.102)
```

### Different Locations
```
Locations:
├── Home Receiver (192.168.1.100)
├── Office Receiver (192.168.1.50)
└── Holiday Home (vpn.holidayhome.com:8080)
```

### Testing Setup
```
Development:
├── Production Box (192.168.1.100)
├── Test Box (192.168.1.200)
└── Development Box (192.168.1.201)
```

## Tips & Best Practices

### Naming Conventions
✅ **Good Names:**
- "Living Room VU+ Ultimo"
- "Bedroom - DM920"
- "Office Receiver"
- "Parents VU+ Solo 4K"

❌ **Avoid:**
- "Box1", "Box2" (not descriptive)
- "192.168.1.100" (use IP as value, not name)
- Very long names that don't fit well

### Security Notes
⚠️ **Password Storage:**
- Passwords stored in plain text in connections.json
- File is in your user AppData folder
- Not encrypted (future enhancement)
- Don't share connections.json file

🛡️ **Recommendations:**
- Use device-specific passwords
- Don't use same password as other services
- Keep computer secure
- Consider changing receiver passwords regularly

### Backup Your Profiles
📁 **Backup Location:**
```
From: %AppData%\E2RecordingManager\connections.json
To:   Your backup location
```

**When to Backup:**
- Before reinstalling Windows
- Before updating application
- After adding many profiles
- Regularly for peace of mind

### Profile Limit
- No hard limit on number of profiles
- Dropdown becomes scrollable with many profiles
- Practical limit: 10-20 profiles for usability

## Troubleshooting

### Profile Not Saving
**Problem:** Click "Save Profile" but profile not appearing in list

**Solutions:**
1. Check profile name is not empty
2. Verify host address is filled in
3. Look for error messages
4. Check AppData folder permissions

### Last Profile Not Loading
**Problem:** Application doesn't remember last profile

**Solutions:**
1. Check if lastprofile.txt exists in AppData
2. Verify profile name in file matches a saved profile
3. Delete lastprofile.txt to reset

### Can't Delete Profile
**Problem:** Delete Profile button does nothing

**Solutions:**
1. Must have profile selected in dropdown
2. Can't delete if it's the only profile
3. Check file permissions on connections.json

### Lost All Profiles
**Problem:** Profiles disappeared after update/restart

**Solutions:**
1. Check `%AppData%\E2RecordingManager\connections.json` exists
2. Look for backup files (.bak extension)
3. May need to recreate profiles manually

## Advanced: Manual Profile Editing

### Editing connections.json Directly
1. Close the application
2. Navigate to `%AppData%\E2RecordingManager\`
3. Open `connections.json` in text editor
4. Edit JSON carefully (maintain structure)
5. Save file
6. Restart application

### Sharing Profiles Between Computers
1. Export: Copy connections.json from Computer A
2. Import: Paste to AppData folder on Computer B
3. Restart application on Computer B
4. All profiles now available

### Resetting Everything
1. Close application
2. Delete entire `E2RecordingManager` folder from AppData
3. Restart application
4. Fresh start with default profile

## Future Enhancements

Planned features for profile management:
- 🔐 Password encryption
- 📤 Export/Import profiles
- 🔍 Network device discovery
- 🧪 Test connection before saving
- 📁 Profile groups/categories
- ☁️ Cloud sync option
- 🔄 Profile templates
- 📊 Connection history
